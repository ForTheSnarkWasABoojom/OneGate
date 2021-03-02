﻿using Newtonsoft.Json;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Series
{
    public class OhlcSeriesDto : SeriesDto
    {
        public override SeriesTypeDto? Type => SeriesTypeDto.OHLC;
        
        [JsonProperty("open")]
        public float Open { get; set; }
        
        [JsonProperty("high")]
        public float High { get; set; }

        [JsonProperty("low")]
        public float Low { get; set; }
        
        [JsonProperty("close")]
        public float Close { get; set; }
    }
}