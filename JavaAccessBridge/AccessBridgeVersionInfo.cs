using System.Runtime.InteropServices;
// ReSharper disable UnassignedGetOnlyAutoProperty
// ReSharper disable UnusedMember.Global

namespace Util.JavaAccessBridge
{

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public readonly struct AccessBridgeVersionInfo 
    {
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.ShortStringSize)] public string VirtualMachineVersion
        {
            get;
        }

        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.ShortStringSize)] public string BridgeJavaClassVersion
        {
            get;
        }

        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.ShortStringSize)] public string BridgeJavaDllVersion
        {
            get;
        }

        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.ShortStringSize)] public string BridgeWinDllVersion
        {
            get;
        }
    }
}
