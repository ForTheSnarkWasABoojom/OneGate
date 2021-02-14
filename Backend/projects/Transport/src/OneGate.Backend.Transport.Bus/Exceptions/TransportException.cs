using System;
using System.Runtime.Serialization;

namespace OneGate.Backend.Transport.Bus.Exceptions
{
    [Serializable]
    public class TransportException : Exception
    {
        public TransportException()
        {
        }

        public TransportException(string message) : base(message)
        {
        }

        public TransportException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TransportException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}