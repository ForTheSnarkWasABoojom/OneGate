using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Services;

namespace OneGate.Backend.Gateway.HealthChecks
{
    public class TimeseriesServiceHealthCheck : IHealthCheck
    {
        private readonly ITimeseriesService _timeseriesService;
        private readonly ILogger<TimeseriesServiceHealthCheck> _logger;

        public TimeseriesServiceHealthCheck(ITimeseriesService timeseriesService, ILogger<TimeseriesServiceHealthCheck> logger)
        {
            _timeseriesService = timeseriesService;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var payload = await _timeseriesService.HealthCheckAsync(new HealthCheckRequest());
                return HealthCheckResult.Healthy($"Ping [{payload.Timestamp}]");
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}