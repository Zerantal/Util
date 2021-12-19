using System.Runtime.InteropServices;

namespace Util.JavaAccessBridge
{
    // an action associated with a component
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal readonly struct AccessibleActionInfo
    {
        public AccessibleActionInfo(string actionName)
        {
            // // Contract.Requires(actionName != null);

            Name = actionName;
        }

        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = JABConstants.ShortStringSize)] public string Name
        {
            get;
        }
    }
}
