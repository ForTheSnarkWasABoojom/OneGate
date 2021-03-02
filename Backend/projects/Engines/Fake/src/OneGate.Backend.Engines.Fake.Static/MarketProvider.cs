using System;
using System.Threading;
using System.Threading.Tasks;
using OneGate.Backend.Engines.Shared.Static.MarketProvider;

namespace OneGate.Backend.Engines.Fake.Static
{
    public class MarketProvider : IMarketProvider
    {
        public event Func<MarketUpdatedArgs, CancellationToken, Task> MarketUpdated;
        
        public MarketProvider()
        {
        }

        public async Task RunAsync(MarketProviderConfiguration configuration, CancellationToken cancellationToken)
        {
        }
    }
}