using System;
using System.Text;
using System.Runtime.InteropServices;
// ReSharper disable CommentTypo

namespace Util.JavaAccessBridge
{
    internal static class UnsafeNativeMethods
    {        
        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Windows_run();
        
        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool isJavaWindow(IntPtr window);
        
        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool getAccessibleContextFromHWND(IntPtr window, out IntPtr vmId, out IntPtr accessibleContext);      

        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool getAccessibleContextInfo(int vmId, IntPtr accessibleContext, out AccessibleContextInfo info);
        
        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool getVersionInfo(int vmId, out AccessBridgeVersionInfo info);        

        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr getAccessibleChildFromContext(int vmId, IntPtr accessibleContext, int childIdx);

        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool getVirtualAccessibleName(int vmId, IntPtr accessibleContext,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name, int nameLength);

        /*
        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl)]
        internal extern static IntPtr getTopLevelObject(Int32 vmID, IntPtr accessibleContext);
        */
        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void releaseJavaObject(int vmId, IntPtr javaObject);

        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr getAccessibleParentFromContext(int vmId, IntPtr accessibleContext);

        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool setTextContents(int vmId, IntPtr accessibleContext, string text);

        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool doAccessibleActions(int vmId, IntPtr accessibleCont, ref AccessibleActionsToDo actionsToDo, out IntPtr failure);
        /*
        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        internal extern static bool getAccessibleKeyBindings(Int32 vmID, IntPtr ac, out AccessibleKeyBindings keyBindings);

        /*
        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        internal extern static bool getAccessibleActions(Int32 vmID, IntPtr ac, IntPtr actionsPtr);
        */
        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool getAccessibleTextInfo(int vmId, IntPtr at, out AccessibleTextInfo textInfo, int x, int y);

        [DllImport("WindowsAccessBridge.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool getAccessibleTextItems(int vmId, IntPtr at, out AccessibleTextItemsInfo textItems, int index);
    }
}
