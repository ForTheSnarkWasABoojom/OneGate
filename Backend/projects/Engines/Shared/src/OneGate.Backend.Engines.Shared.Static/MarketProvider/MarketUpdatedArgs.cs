using System.Collections.Generic;
using OneGate.Backend.Engines.Shared.Static.Domain;

namespace OneGate.Backend.Engines.Shared.Static.MarketProvider
{
    public class MarketUpdatedArgs
    {
        public int AssetId { get; set; }
        public List<OhlcSeries> Ohlc { get; set; }
    }
}