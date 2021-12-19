using System.Runtime.InteropServices;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace Util.JavaAccessBridge
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal struct AccessibleTextItemsInfo
    {
        public char Letter { get; }
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.ShortStringSize)] public string Word
        {
            get;
        }

        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.MaxStringSize)] public string Sentence
        {
            get;
        }
    }
}
