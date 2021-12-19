using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace Util.JavaAccessBridge
{
    [Serializable]
    public class JABException : Exception
    {
        public JABException()
        {        
        }

        public JABException(string message)
            : base(message)
        {           
        }

        public JABException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // This constructor is needed for serialization.
        protected JABException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
