using System.Collections.Generic;
using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Core.Timeseries.Contracts.Series
{
    [EntityName("response.series")]
    public class SeriesResponse 
    {
        [JsonProperty("series")]
        public IEnumerable<SeriesDto> Series { get; set; }
    }
}