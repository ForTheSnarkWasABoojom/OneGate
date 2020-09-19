using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Services;

namespace OneGate.Backend.Gateway.HealthChecks
{
    public class OhlcServiceHealthCheck : IHealthCheck
    {
        private readonly IOhlcService _ohlcService;
        private readonly ILogger<OhlcServiceHealthCheck> _logger;

        public OhlcServiceHealthCheck(IOhlcService ohlcService, ILogger<OhlcServiceHealthCheck> logger)
        {
            _ohlcService = ohlcService;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var payload = await _ohlcService.HealthCheckAsync(new HealthCheckRequest());
                return HealthCheckResult.Healthy($"Ping [{payload.Timestamp}]");
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}