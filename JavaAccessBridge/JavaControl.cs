using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics;
// ReSharper disable UnusedMember.Global

namespace Util.JavaAccessBridge
{
    public struct JavaControlRole
    {
        public const string Text = "text";
        public const string PasswordText = "password text";
        public const string Button = "push button";
        public const string Label = "label";
        public const string InternalFrame = "internal frame";
        public const string Table = "table";
        public const string Unspecified = "";
    }

    public class JavaControl
    {
        internal JavaControl(int vmId, IntPtr ac)
        {
            if (vmId <= 0) throw new ArgumentOutOfRangeException(nameof(vmId));
            // // Contract.Requires(JAB.IsJABInitialized);

            VirtualMachineId = vmId;
            AccessibleContext = ac;
        }

        private static JavaControl CreateControl(int vmId, IntPtr ac)
        {
            // // Contract.Requires(JAB.IsJABInitialized);

            var success = UnsafeNativeMethods.getAccessibleContextInfo(vmId, ac, out var info);
            // Contract.Assert(JAB.IsJABInitialized);
            if (success)
                return CreateControlUsingRole(vmId, ac, info.Role);

            Trace.TraceWarning("Call to getAccessibleContextInfo failed.");
            return new JavaControl(-1, IntPtr.Zero);
        }

        private static JavaControl CreateControlUsingRole(int vmId, IntPtr ac, string role)
        {
            // // Contract.Requires(JAB.IsJABInitialized);

            switch (role)
            {
                case JavaControlRole.Button:
                    return new JavaButtonControl(vmId, ac);

                case JavaControlRole.PasswordText:
                case JavaControlRole.Text:
                    return new JavaTextInputControl(vmId, ac);

                default:
                    return new JavaControl(vmId, ac);                  
            }
        }

        // return first node found matching search criteria
        public JavaControl FindChildControl(string name, string role)
        {
            // // Contract.Requires(name != null);
          
            Queue<IntPtr> searchList = new Queue<IntPtr>();

            IntPtr childAc;
            StringBuilder controlName = new StringBuilder(JABConstants.ShortStringSize);

            // enqueue all child controls first
            var info = AccessibleContextInfo;  
                                   
                // add children controls to search queue
            for (int i = 0; i < info.ChildrenCount; i++)
            {
                childAc = UnsafeNativeMethods.getAccessibleChildFromContext(VirtualMachineId, AccessibleContext, i);
                if (childAc != IntPtr.Zero)
                {
                    searchList.Enqueue(childAc);
                }
            }
            
            while (searchList.Count != 0)
            {
                var currentAc = searchList.Dequeue();
                // get context info
                var success = UnsafeNativeMethods.getAccessibleContextInfo(VirtualMachineId, currentAc, out info);
                if (!success) continue;

                success = UnsafeNativeMethods.getVirtualAccessibleName(VirtualMachineId, currentAc, controlName, JABConstants.ShortStringSize);
                if (!success) continue;

                if (name.Equals(controlName.ToString()) && info.Role.Equals(role))
                    return CreateControlUsingRole(VirtualMachineId, currentAc, role);

                // add children controls to search queue
                for (int i = 0; i < info.ChildrenCount; i++)
                {
                    childAc = UnsafeNativeMethods.getAccessibleChildFromContext(VirtualMachineId, currentAc, i);
                    if (childAc != IntPtr.Zero)
                    {
                        searchList.Enqueue(childAc);
                    }
                }
            }
            return null;
        }

        public JavaControl GetParent()
        {
            IntPtr parentAc = UnsafeNativeMethods.getAccessibleParentFromContext(VirtualMachineId, AccessibleContext);

            return parentAc == IntPtr.Zero ? null : CreateControl(VirtualMachineId, parentAc);
        }

        public JavaControl GetChild(int childIndex)
        {
            IntPtr childAc = UnsafeNativeMethods.getAccessibleChildFromContext(VirtualMachineId, AccessibleContext, childIndex);

            return CreateControl(VirtualMachineId, childAc);
        }

        public ReadOnlyCollection<JavaControl> Children
        {          
            get
            {
                List<JavaControl> childControls = new List<JavaControl>();
                AccessibleContextInfo info = AccessibleContextInfo;

                for (int i = 0; i < info.ChildrenCount; i++)
                {
                    var childAc = UnsafeNativeMethods.getAccessibleChildFromContext(VirtualMachineId, AccessibleContext, i);

                    if (!childAc.Equals(IntPtr.Zero))
                        childControls.Add(new JavaControl(VirtualMachineId, childAc));
                    else
                        Trace.TraceWarning("Call to getAccessibleChildFromContext failed.");
                }

                return new ReadOnlyCollection<JavaControl>(childControls);
            }
        }

        public int VirtualMachineId { get; }

        public bool IsVisible
        {
            get
            {
                AccessibleContextInfo info = AccessibleContextInfo;

                return info.States.Contains("visible");
            }
        }

        public int ChildCount
        {
            get
            {
                AccessibleContextInfo info = AccessibleContextInfo;

                return info.ChildrenCount;
            }
        }

        public string Name
        {
            get
            {
                AccessibleContextInfo info = AccessibleContextInfo;
                return info.Name;
            }
        }

        private AccessibleContextInfo AccessibleContextInfo
        {
            get
            {
                bool success = UnsafeNativeMethods.getAccessibleContextInfo(VirtualMachineId, AccessibleContext, out var info);

                if (success)
                    return info;

                Trace.TraceError("Call to getAccessibleContextInfo failed.");
                return new AccessibleContextInfo();

            }
        }
       

        protected IntPtr AccessibleContext { get; }

        ~JavaControl()
        {
            UnsafeNativeMethods.releaseJavaObject(VirtualMachineId, AccessibleContext);
        }
    }
}
