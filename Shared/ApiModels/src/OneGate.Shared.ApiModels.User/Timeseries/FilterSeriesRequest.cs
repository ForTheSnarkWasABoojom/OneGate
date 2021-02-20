using System;
using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Shared.ApiModels.User.Timeseries
{
    public class FilterSeriesRequest : FilterRequest
    {
        [FromQuery(Name = "layout_id")]
        public int LayoutId { get; set; }

        [FromQuery(Name = "end_timestamp")]
        public DateTime? EndTimestamp { get; set; }

        [FromQuery(Name = "start_timestamp")]
        public DateTime? StartTimestamp { get; set; }
    }
}