using System;
using System.Runtime.Serialization;

namespace OneGate.Frontend.ClientLibrary
{
    [Serializable]
    public class OneGateApiException : Exception
    {
        public OneGateApiException()
        {
        }

        public OneGateApiException(string message) : base(message)
        {
        }

        public OneGateApiException(string message, Exception inner) : base(message, inner)
        {
        }

        protected OneGateApiException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}