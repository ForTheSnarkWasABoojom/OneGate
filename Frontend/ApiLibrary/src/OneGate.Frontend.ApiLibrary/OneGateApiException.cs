using System;
using System.Runtime.Serialization;

namespace OneGate.Frontend.ApiLibrary
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