using System.Collections.Generic;
using System.Runtime.InteropServices;
// ReSharper disable UnusedMember.Global
// ReSharper disable UnassignedGetOnlyAutoProperty

namespace Util.JavaAccessBridge
{
	// all of the actions associated with a component
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    // ReSharper disable once UnusedMember.Global
    internal class AccessibleActions 
	{

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = JABConstants.MaxActionInfo, ArraySubType = UnmanagedType.Struct)]
		private readonly AccessibleActionInfo[] _actionInfo;	// the action information

		public AccessibleActions()
		{
			_actionInfo = new AccessibleActionInfo[JABConstants.MaxActionInfo];
		}


        public int ActionsCount { get; }

        public ICollection<AccessibleActionInfo> ActionInfo => _actionInfo;
    }
	
}