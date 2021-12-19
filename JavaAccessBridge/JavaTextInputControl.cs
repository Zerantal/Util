using System;
using System.Diagnostics;

namespace Util.JavaAccessBridge
{
    public class JavaTextInputControl : JavaControl
    {
        internal JavaTextInputControl(int vmId, IntPtr ac)
            : base(vmId, ac)
        {
            // // Contract.Requires(JAB.IsJABInitialized);                
        }

        public string Text 
        {       
            
            get
            {
                bool result = UnsafeNativeMethods.getAccessibleTextInfo(VirtualMachineId,
                    AccessibleContext, out var info, 0, 0);

                if (!result)
                {
                    Trace.TraceWarning("Call to getAccessibleTextInfo failed.");
                    return "";
                }

                result = UnsafeNativeMethods.getAccessibleTextItems(VirtualMachineId,
                    AccessibleContext, out var textItems, info.IndexAtPoint);

                if (result) return textItems.Sentence;

                Trace.TraceWarning("Call to getAccessibleTextItems failed.");
                return "";

            }

            set
            {
                bool result = UnsafeNativeMethods.setTextContents(VirtualMachineId, AccessibleContext, value);

                if (!result)
                {
                    Trace.TraceWarning("Call to setTextContents failed.");                    
                }
            }
        }
    }
}
