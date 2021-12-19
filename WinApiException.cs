using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
// ReSharper disable UnusedMember.Global

namespace Util
{
    [Serializable]
    public class WinApiException : Exception
    {
        private string _sourceDll;
        private string _invokedFunction;
        private int _errorCode;

        public WinApiException()
        {           
        }

        public WinApiException(string message) : base(message)
        {         
        }

        public WinApiException(string message, Exception inner) : base(message, inner)
        {          
        }

        // This constructor is needed for serialization.
        protected WinApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            // // Contract.Requires(info != null);

            _sourceDll = info.GetString("SourceDLL");
            _invokedFunction = info.GetString("InvokedFunction");            
        }

        public WinApiException(string message, string sourceDll, string invokedFunction, int errorCode) 
            : base(message)
        {
            _sourceDll = sourceDll;
            _invokedFunction = invokedFunction;
            _errorCode = errorCode;
        }

        public string InvokedFunction
        {
            get => _invokedFunction;
            set => _invokedFunction = value;
        }


        public string SourceDll
        {
            get => _sourceDll;
            set => _sourceDll = value;
        }

        public int ErrorCode
        {
            get => _errorCode;
            set => _errorCode = value;
        }

        #region ISerializable Members

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("SourceDLL", _sourceDll);
            info.AddValue("InvokedFunction", _invokedFunction);
        }

        #endregion
    }
}
