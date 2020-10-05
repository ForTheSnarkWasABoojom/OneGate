using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Rpc.Services;

namespace OneGate.Backend.Gateway.HealthChecks.Engine
{
    public class FakeEngineHealthCheck : IHealthCheck
    {
        private readonly IEngineService _engineService;
        private readonly ILogger<FakeEngineHealthCheck> _logger;

        public FakeEngineHealthCheck(IEngineService engineService, ILogger<FakeEngineHealthCheck> logger)
        {
            _engineService = engineService;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return HealthCheckResult.Healthy();
        }
    }
}