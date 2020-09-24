using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.EventHubs;

namespace OneGate.Backend.Gateway.EventListeners
{
    public class AccountEventListener : IHostedService
    {
        private readonly ILogger<AccountEventListener> _logger;
        private readonly IHubContext<AccountEventHub> _hubContext;
        
        private readonly IBus _bus;

        public AccountEventListener(ILogger<AccountEventListener> logger, 
            IBus bus, IHubContext<AccountEventHub> hubContext)
        {
            _logger = logger;
            _bus = bus;
            _hubContext = hubContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account event listener started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Account event listener stopped");
        }
    }
}