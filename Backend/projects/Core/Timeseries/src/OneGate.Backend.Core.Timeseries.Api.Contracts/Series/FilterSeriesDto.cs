using System;
using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Series
{
    public class FilterSeriesDto : FilterDto
    {
        [FromQuery(Name = "layout_id")]
        public int LayoutId { get; set; }

        [FromQuery(Name = "end_timestamp")]
        public DateTime? EndTimestamp { get; set; }

        [FromQuery(Name = "start_timestamp")]
        public DateTime? StartTimestamp { get; set; }
    }
}