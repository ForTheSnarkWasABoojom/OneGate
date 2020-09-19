using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Rpc.Contracts.Base.HealthCheck;
using OneGate.Backend.Rpc.Services;

namespace OneGate.Backend.Gateway.HealthChecks
{
    public class AccountServiceHealthCheck : IHealthCheck
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountServiceHealthCheck> _logger;

        public AccountServiceHealthCheck(IAccountService accountService, ILogger<AccountServiceHealthCheck> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var payload = await _accountService.HealthCheckAsync(new HealthCheckRequest());
                return HealthCheckResult.Healthy($"Ping [{payload.Timestamp}]");
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}