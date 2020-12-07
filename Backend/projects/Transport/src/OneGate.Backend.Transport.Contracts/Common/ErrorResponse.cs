using MassTransit.Topology;

namespace OneGate.Backend.Transport.Contracts.Common
{
    [EntityName("response.error")]
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string InnerExceptionMessage { get; set; }
    }
}