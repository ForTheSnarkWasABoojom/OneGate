using System;
using MassTransit.Topology;
using Newtonsoft.Json;
using OneGate.Backend.Core.Base.Contracts;

namespace OneGate.Backend.Core.Timeseries.Contracts.Series
{
    [EntityName("request.series.get")]
    public class GetSeries : LargeDataRequest
    {
        [JsonProperty("layout_id")]
        public int LayoutId { get; set; }

        [JsonProperty("end_timestamp")]
        public DateTime? EndTimestamp { get; set; }

        [JsonProperty("start_timestamp")]
        public DateTime? StartTimestamp { get; set; }
    }
}