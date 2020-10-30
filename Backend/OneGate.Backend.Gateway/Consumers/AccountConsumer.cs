using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.EventHubs;

namespace OneGate.Backend.Gateway.Consumers
{
    public class AccountConsumer : IConsumer
    {
        private readonly ILogger<AccountConsumer> _logger;
        private readonly IHubContext<AccountEventHub> _hubContext;

        public AccountConsumer(ILogger<AccountConsumer> logger, IHubContext<AccountEventHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
    }
}