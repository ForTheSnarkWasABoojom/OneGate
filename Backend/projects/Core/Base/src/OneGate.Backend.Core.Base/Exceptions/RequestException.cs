using System;
using System.Runtime.Serialization;

namespace OneGate.Backend.Core.Base.Exceptions
{
    [Serializable]
    public class RequestException : Exception
    {
        public int StatusCode { get; set; } = 500;
        public string InnerExceptionMessage { get; set; }
        
        public RequestException()
        {
        }

        public RequestException(string message) : base(message)
        {
        }

        public RequestException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected RequestException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}