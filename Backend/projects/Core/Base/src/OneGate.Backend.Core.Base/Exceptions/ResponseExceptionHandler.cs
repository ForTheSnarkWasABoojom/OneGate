using EntityFramework.Exceptions.Common;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Bus.Contracts;

namespace OneGate.Backend.Core.Base.Exceptions
{
    public class ResponseExceptionHandler : IResponseExceptionHandler
    {
        public ErrorResponse CreateErrorResponse(System.Exception exception)
        {
            return exception switch
            {
                RequestException ex => new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                    InnerExceptionMessage = ex.InnerExceptionMessage
                },
                UniqueConstraintException ex => new ErrorResponse
                {
                    StatusCode = 409,
                    Message = "Object has duplicates",
                    InnerExceptionMessage = ex.Message
                },
                ReferenceConstraintException ex => new ErrorResponse
                {
                    StatusCode = 424,
                    Message = "Object has wrong dependencies",
                    InnerExceptionMessage = ex.Message
                },
                { } ex => new ErrorResponse
                {
                    StatusCode = 500,
                    Message = "Unknown error occured",
                    InnerExceptionMessage = ex.Message
                }
            };
        }
    }
}