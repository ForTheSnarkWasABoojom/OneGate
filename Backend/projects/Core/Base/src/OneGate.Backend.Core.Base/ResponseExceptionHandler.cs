using System;
using EntityFramework.Exceptions.Common;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Contracts;
using OneGate.Backend.Transport.Bus.Exceptions;

namespace OneGate.Backend.Core.Base
{
    public class ResponseExceptionHandler : IResponseExceptionHandler
    {
        public ErrorResponse CreateErrorResponse(Exception exception)
        {
            return exception switch
            {
                
                ReferenceConstraintException ex => new ErrorResponse
                {
                    StatusCode = 424,
                    Message = "Entity has wrong dependencies",
                    InnerExceptionMessage = ex.Message
                },
                { } ex => throw ex
            };
        }
    }
}