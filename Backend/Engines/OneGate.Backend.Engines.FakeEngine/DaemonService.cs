using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OneGate.Backend.Engines.FakeEngine
{
    public class DaemonService : IHostedService
    {
        private readonly ILogger<DaemonService> _logger;
        private readonly IBus _bus;

        public DaemonService(ILogger<DaemonService> logger, IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fake engine daemon service started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fake engine daemon service stopped");
        }
    }
}