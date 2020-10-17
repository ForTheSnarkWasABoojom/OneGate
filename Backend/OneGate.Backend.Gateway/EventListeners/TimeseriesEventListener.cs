using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OneGate.Backend.Gateway.EventHubs;
using OneGate.Backend.Rpc.Contracts.Timeseries;
using Serilog;

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

            // Register subscribers.
            _bus.SubscribeAsync<OnOhlcTimeseriesChanged>("gateway", OnOhlcTimeseriesChanged);
        }

        public async Task OnOhlcTimeseriesChanged(OnOhlcTimeseriesChanged model)
        {
            await _hubContext.Clients.Group($"ohlc.{model.AssetId}")
                .SendAsync("on_ohlc_timeseries_changed", model.OhlcByInterval);
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