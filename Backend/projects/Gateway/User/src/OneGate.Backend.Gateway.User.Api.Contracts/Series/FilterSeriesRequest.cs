using System;
using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Series
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