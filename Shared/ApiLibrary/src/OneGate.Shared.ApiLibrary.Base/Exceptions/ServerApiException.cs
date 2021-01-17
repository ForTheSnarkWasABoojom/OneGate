using System;
using System.Runtime.Serialization;

namespace OneGate.Shared.ApiLibrary.Base.Exceptions
{
    [Serializable]
    public class ServerApiException : ApiException
    {
        public ServerApiException()
        {
        }

        public ServerApiException(string message) : base(message)
        {
        }

        public ServerApiException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ServerApiException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}