using System.Runtime.InteropServices;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace Util.JavaAccessBridge
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal readonly struct AccessibleContextInfo
    {
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.MaxStringSize)] public string Name { get; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.MaxStringSize)] public string Description
        {
            get;
        }

        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.ShortStringSize)] public string Role
        {
            get;
        }

        // ReSharper disable once InconsistentNaming
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.ShortStringSize)] public string RoleENUS
        {
            get;
        }

        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.ShortStringSize)] public string States
        {
            get;
        }

        // ReSharper disable once InconsistentNaming
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.ShortStringSize)] public string StatesENUS
        {
            get;
        }

        [field: MarshalAs(UnmanagedType.I4)] public int IndexInParent { get; }
        [field: MarshalAs(UnmanagedType.I4)] public int ChildrenCount { get; }
        [field: MarshalAs(UnmanagedType.I4)] public int X { get; }
        [field: MarshalAs(UnmanagedType.I4)] public int Y { get; }
        [field: MarshalAs(UnmanagedType.I4)] public int Width { get; }
        [field: MarshalAs(UnmanagedType.I4)] public int Height { get; }
        [field: MarshalAs(UnmanagedType.Bool)] public bool AccessibleComponent { get; }
        [field: MarshalAs(UnmanagedType.Bool)] public bool AccessibleAction { get; }
        [field: MarshalAs(UnmanagedType.Bool)] public bool AccessibleSelection { get; }
        [field: MarshalAs(UnmanagedType.Bool)] public bool AccessibleText { get; }
        [field: MarshalAs(UnmanagedType.Bool)] public bool AccessibleInterfaces { get; }
    }
}
