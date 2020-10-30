using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.EventHubs;

namespace OneGate.Backend.Gateway.Consumers
{
    public class TimeseriesConsumer : IConsumer
    {
        private readonly ILogger<TimeseriesConsumer> _logger;
        private readonly IHubContext<TimeseriesEventHub> _hubContext;

        public TimeseriesConsumer(ILogger<TimeseriesConsumer> logger, IHubContext<TimeseriesEventHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }
    }
}