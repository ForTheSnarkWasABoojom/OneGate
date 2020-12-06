using System;
using System.Collections.Generic;
using OneGate.Shared.Models.Series.Ohlc;

namespace OneGate.Backend.Engines.Base.OhlcProvider
{
    public class OhlcProviderEventArgs : EventArgs
    {
        public readonly Dictionary<IntervalDto, OhlcDto> OhlcByInterval;

        public OhlcProviderEventArgs(Dictionary<IntervalDto, OhlcDto> ohlcByInterval)
        {
            OhlcByInterval = ohlcByInterval;
        }
    }
}