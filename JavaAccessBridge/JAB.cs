using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Diagnostics;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

namespace Util.JavaAccessBridge
{
    public static class JAB
    {
        public static void InitializeJAB()
        {
            if (IsJABInitialized)
                return;

            if (Application.MessageLoop == false)
                throw new JABException("Unable to initialize java access bridge: no message loop " +
                    "running on thread.");
            UnsafeNativeMethods.Windows_run();
            Application.DoEvents(); // complete initialisation

            IsJABInitialized = true;
        }

        public static bool IsJABInitialized { get; private set; }

        #region Convenient methods

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public static ReadOnlyCollection<JavaControl> GetJavaWindows()
        {
            // //Contract.Requires<JABException>(JAB.IsJABInitialized, "Java Access Bridge is not initialized");           

            List<JavaControl> controls = new List<JavaControl>();

            ReadOnlyCollection<Tuple<string, IntPtr>> desktopWindows = WinApiWrapper.GetDesktopWindows();

            foreach (var (_, wndHandle) in desktopWindows)
            {
                // is window a java window?
                var success = UnsafeNativeMethods.isJavaWindow(wndHandle);
                if (!success) continue;
                // get accessible context
                success = UnsafeNativeMethods.getAccessibleContextFromHWND(wndHandle, out var vmIDPtr, out var ac);
                if (success)
                {
                    controls.Add(new JavaControl(vmIDPtr.ToInt32(), ac));
                }
            }

            return new ReadOnlyCollection<JavaControl>(controls);
        }

        public static JavaControl GetJavaControlFromHWND(IntPtr windowHandle)
        {
            // //Contract.Ensures(// Contract.Result<JavaControl>() != null);

            if (!IsJABInitialized)
                throw new JABException("Java Access Bridge is not initialized.");

            // is window handle valid?
            if (WinApiWrapper.IsValidWindow(windowHandle) == false)
                throw new JABException("Window handle does not identify a window.");

            // is window a java window?
            var success = UnsafeNativeMethods.isJavaWindow(windowHandle);
            if (!success)
            {
                throw new JABException("Window handle does not identify a java window.");
            }

            // get accessible context
            success = UnsafeNativeMethods.getAccessibleContextFromHWND(windowHandle, out var vmIDPtr, out var ac);
            if (!success)
            {
               throw new JABException("Unable to acquire the accessible context for window!");
            }            
            
            return new JavaControl(vmIDPtr.ToInt32(), ac);
        }    

        public static AccessBridgeVersionInfo GetVersionInfo(int virtualMachineId)
        {
            bool success = UnsafeNativeMethods.getVersionInfo(virtualMachineId, out var versionInfo);

            if (success) return versionInfo;
            Trace.TraceWarning("Call to getVersionInfo failed.");
            return new AccessBridgeVersionInfo();

        }

        #endregion
    }
}
