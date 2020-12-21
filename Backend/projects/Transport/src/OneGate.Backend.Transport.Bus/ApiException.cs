using System;
using System.Runtime.Serialization;

namespace OneGate.Backend.Transport.Bus
{
    [Serializable]
    public class ApiException : Exception
    {
        public int StatusCode { get; set; } = 500;
        public string InnerExceptionMessage { get; set; }

        public ApiException()
        {
        }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public ApiException(string message, int statusCode, string innerExceptionMessage) : base(message)
        {
            StatusCode = statusCode;
            InnerExceptionMessage = innerExceptionMessage;
        }

        public ApiException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ApiException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}