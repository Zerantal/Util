using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Collections.ObjectModel;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Local

namespace Util
{
    public static class WinApiWrapper
    {
        public const int PostMouseDelay = 250; //quarter of a second
        private const int MAXTITLE = 255;

        private const int MOUSEEVENTF_MOVE = 0x00000001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x00000002;
        private const int MOUSEEVENTF_LEFTUP = 0x00000004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x00000008;
        private const int MOUSEEVENTF_RIGHTUP = 0x00000010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x00000020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x00000040;
        private const int MOUSEEVENTF_WHEEL = 0x00000800;
        private const int MOUSEEVENTF_ABSOLUTE = 0x00008000;



        private static bool AddWindowTitleToList(IntPtr hWnd, ref object windowList)
        {
            // // Contract.Requires(windowList != null);
            // // Contract.Requires(windowList is List<Tuple<string, IntPtr>>);

            List<Tuple<string, IntPtr>> openWindows = windowList as List<Tuple<string, IntPtr>>;
  //          // Contract.Assume(openWindows != null);

            string title = GetWindowText(hWnd);
            if (title != null)
                openWindows?.Add(new Tuple<string, IntPtr>(title, hWnd));
            return true;
        }
       
        /// <summary>
        /// Returns the caption of a windows by given HWND identifier.
        /// </summary>
        public static string GetWindowText(IntPtr hWnd)
        {
            StringBuilder title = new StringBuilder(MAXTITLE);
            int titleLength = NativeMethods.GetWindowText(hWnd, title, title.Capacity + 1);
            int errorCode = Marshal.GetLastWin32Error();

            if (titleLength <= 0)
            {
                if (errorCode != 0x0)
                    return null;
            }
            else
                title.Length = titleLength;

            return title.ToString();
        }

        public static ReadOnlyCollection<Tuple<string, IntPtr>> GetDesktopWindows()
        {
            object openWindows = new List<Tuple<string, IntPtr>>();
           
            NativeMethods.EnumDelegate enumfunc = AddWindowTitleToList;
            IntPtr hDesktop = IntPtr.Zero; // current desktop
            bool success = NativeMethods.EnumDesktopWindows(hDesktop, enumfunc, ref openWindows);

            if (!success)
            {
                // Get the last Win32 error code
                int errorCode = Marshal.GetLastWin32Error();

                throw new WinApiException("Failed to enumerate desktop windows.", "user32.dll",
                    "EnumDesktopWindows", errorCode); 

            }

            if (openWindows is List<Tuple<string, IntPtr>> retList)
                return new ReadOnlyCollection<Tuple<string, IntPtr>>(retList);

            return new ReadOnlyCollection<Tuple<string, IntPtr>>(new List<Tuple<string, IntPtr>>());
        }

        // ReSharper disable once UnusedMember.Global
        public static void MouseLeftClick(int xValue, int yValue)
        {            
            Cursor.Position = new Point(xValue, yValue);
            NativeMethods.mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero);
            NativeMethods.mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero);
            Thread.Sleep(PostMouseDelay);
        }

        public static bool IsValidWindow(IntPtr windowHandle)
        {
            return NativeMethods.IsWindow(windowHandle);            
        }

        public static bool SetForegroundWindow(IntPtr winHandle)
        {
            return NativeMethods.SetForegroundWindow(winHandle);                        
        }

        public static Rectangle GetWindowRectangle(IntPtr winHandle)
        {
            if (NativeMethods.GetWindowRect(winHandle, out var winRect)) return winRect.ToRectangle();

            // Get the last Win32 error code
            int errorCode = Marshal.GetLastWin32Error();

            throw new WinApiException("Failed to get window dimensions.", "user32.dll",
                "GetWindowRect", errorCode);

        }

        public static IntPtr FindWindow(string caption)
        {
            IntPtr winHandle = NativeMethods.FindWindow(null,caption);

            return winHandle;
        }
    }
}
