using System;

namespace OneGate.Backend.Rpc.Contracts.Base.HealthCheck
{
    public class HealthCheckResponse : SuccessResponse
    {
        public DateTime Timestamp { get; set; }
    }
}