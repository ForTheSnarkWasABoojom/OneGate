using System;
using System.Runtime.Serialization;

namespace OneGate.Backend.Transport.Bus.Exceptions
{
    [Serializable]
    public class TransportTimeoutException : TransportException
    {
        public TransportTimeoutException()
        {
        }

        public TransportTimeoutException(string message) : base(message)
        {
        }

        public TransportTimeoutException(string message, Exception inner) : base(message, inner)
        {
        }

        protected TransportTimeoutException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}