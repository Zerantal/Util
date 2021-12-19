using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace Util.JavaAccessBridge
{
    // all of the key bindings associated with a component
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    // ReSharper disable once UnusedMember.Global
    internal class AccessibleKeyBindings
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = JABConstants.MaxKeyBindings)]
	    private readonly AccessibleKeyBindingInfo[] _keyBindingInfo;

        public AccessibleKeyBindings()
        {
            KeyBindingsCount = 0;
            _keyBindingInfo = new AccessibleKeyBindingInfo[JABConstants.MaxKeyBindings];
        }

        public int KeyBindingsCount { get; }

        public ReadOnlyCollection<AccessibleKeyBindingInfo> KeyBindingInfo
        {
            get
            {
                var bindings = new List<AccessibleKeyBindingInfo>(KeyBindingsCount);
                for (int i = 0; i < KeyBindingsCount; i++)
                    bindings.Add(_keyBindingInfo[i]);

                return new ReadOnlyCollection<AccessibleKeyBindingInfo>(bindings);
            }
        }

    }

    // a key binding associates with a component
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AccessibleKeyBindingInfo 
    {
        public char Character { get; }
        public int Modifiers { get; }
    }
}
