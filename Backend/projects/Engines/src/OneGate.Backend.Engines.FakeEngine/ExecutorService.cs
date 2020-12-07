using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OneGate.Backend.Engines.FakeEngine
{
    public class ExecutorService : IHostedService
    {
        private readonly ILogger<ExecutorService> _logger;

        public ExecutorService(ILogger<ExecutorService> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fake engine executor service started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fake engine executor service stopped");
        }
    }
}