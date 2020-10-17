using System;
using System.Collections.Generic;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Engines.Base.OhlcProvider
{
    public class OhlcProviderEventArgs : EventArgs
    {
        public readonly Dictionary<OhlcIntervalDto, OhlcTimeseriesDto> OhlcByInterval;

        public OhlcProviderEventArgs(Dictionary<OhlcIntervalDto, OhlcTimeseriesDto> ohlcByInterval)
        {
            OhlcByInterval = ohlcByInterval;
        }
    }
}