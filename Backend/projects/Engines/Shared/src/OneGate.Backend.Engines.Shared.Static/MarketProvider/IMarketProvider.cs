using System;
using System.Threading;
using System.Threading.Tasks;

namespace OneGate.Backend.Engines.Shared.Static.MarketProvider
{
    public interface IMarketProvider
    {
        public event Func<MarketUpdatedArgs, CancellationToken, Task> MarketUpdated;
        public Task RunAsync(MarketProviderConfiguration configuration, CancellationToken cancellationToken);
    }
}