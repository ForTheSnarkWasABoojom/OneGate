using System;
using OneGate.Backend.Transport.Bus.Contracts;

namespace OneGate.Backend.Transport.Bus
{
    public interface IResponseExceptionHandler
    {
        public ErrorResponse CreateErrorResponse(Exception exception);
    }
}