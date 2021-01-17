using System;
using System.Runtime.Serialization;

namespace OneGate.Shared.ApiLibrary.Base.Exceptions
{
    [Serializable]
    public class ClientApiException : ApiException
    {
        public ClientApiException()
        {
        }

        public ClientApiException(string message) : base(message)
        {
        }

        public ClientApiException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ClientApiException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}