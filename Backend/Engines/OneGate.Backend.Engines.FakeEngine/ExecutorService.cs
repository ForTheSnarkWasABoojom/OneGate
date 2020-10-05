using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Rpc.Services;

namespace OneGate.Backend.Engines.FakeEngine
{
    public class ExecutorService : IHostedService, IEngineService
    {
        private readonly ILogger<ExecutorService> _logger;
        private readonly IBus _bus;

        public ExecutorService(ILogger<ExecutorService> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
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