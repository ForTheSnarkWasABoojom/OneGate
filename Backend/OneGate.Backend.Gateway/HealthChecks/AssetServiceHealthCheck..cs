using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Services;

namespace OneGate.Backend.Gateway.HealthChecks
{
    public class AssetServiceHealthCheck : IHealthCheck
    {
        private readonly IAssetService _assetService;
        private readonly ILogger<AssetServiceHealthCheck> _logger;

        public AssetServiceHealthCheck(IAssetService assetService, ILogger<AssetServiceHealthCheck> logger)
        {
            _assetService = assetService;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var payload = await _assetService.HealthCheckAsync(new HealthCheckRequest());
                return HealthCheckResult.Healthy($"Ping [{payload.Timestamp}]");
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}