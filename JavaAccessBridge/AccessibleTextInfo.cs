using System.Runtime.InteropServices;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace Util.JavaAccessBridge
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct AccessibleTextInfo
    {
        public int CharCount { get; }
        public int CaretIndex { get; }
        public int IndexAtPoint { get; }
    }
}
