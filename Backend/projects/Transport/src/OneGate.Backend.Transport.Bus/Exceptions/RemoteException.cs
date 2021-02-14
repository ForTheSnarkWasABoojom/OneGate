using System;
using System.Runtime.Serialization;

namespace OneGate.Backend.Transport.Bus.Exceptions
{
    [Serializable]
    public class RemoteException : Exception
    {
        public int StatusCode { get; set; } = 500;
        public string InnerExceptionMessage { get; set; }

        public RemoteException()
        {
        }

        public RemoteException(string message) : base(message)
        {
        }

        public RemoteException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public RemoteException(string message, int statusCode, string innerExceptionMessage) : base(message)
        {
            StatusCode = statusCode;
            InnerExceptionMessage = innerExceptionMessage;
        }

        public RemoteException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RemoteException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}