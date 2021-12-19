using System;
// ReSharper disable UnusedMember.Global

namespace Util.JavaAccessBridge
{
    public class JavaButtonControl : JavaControl
    {

        internal JavaButtonControl(int vmId, IntPtr ac)
            : base(vmId, ac)
        {
            // // Contract.Requires(JAB.IsJABInitialized);
        }

        public void Click()
        {
            AccessibleActionsToDo actions = new AccessibleActionsToDo();
            // Contract.Assume(actions.ActionsCount == 0);
            actions.AddAction("click");

            bool result = UnsafeNativeMethods.doAccessibleActions(VirtualMachineId, AccessibleContext, ref actions, out _);

            if (result == false)
                throw new JABException("Click action failed.");            
        }
    }
}
