using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.EventHubs;

namespace OneGate.Backend.Gateway.EventListeners
{
    public class TimeseriesEventListener : IHostedService
    {
        private readonly ILogger<TimeseriesEventListener> _logger;
        private readonly IHubContext<TimeseriesEventHub> _hubContext;

        private readonly IBus _bus;

        public TimeseriesEventListener(ILogger<TimeseriesEventListener> logger,
            IBus bus, IHubContext<TimeseriesEventHub> hubContext)
        {
            _logger = logger;
            _bus = bus;
            _hubContext = hubContext;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timeseries event listener started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timeseries event listener stopped");
        }
    }
}