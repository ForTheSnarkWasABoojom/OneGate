using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OneGate.Shared.Models.Ohlc;

namespace OneGate.Backend.Gateway.EventHubs
{
    public class TimeseriesEventHub : Hub
    {
        [HubMethodName("subscribe_ohlc_timeseries")]
        public async Task SubscribeOhlcTimeseries(int assetId, OhlcIntervalDto interval)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"ohlc.{assetId.ToString()}.{interval.ToString()}");
        }
        
        [HubMethodName("unsubscribe_ohlc_timeseries")]
        public async Task UnsubscribeOhlcTimeseries(int assetId, OhlcIntervalDto interval)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"ohlc.{assetId.ToString()}.{interval.ToString()}");
        }
    }
}