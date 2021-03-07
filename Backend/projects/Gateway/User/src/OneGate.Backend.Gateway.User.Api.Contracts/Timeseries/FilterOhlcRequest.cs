using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.User.Api.Contracts.Timeseries
{
    public class FilterOhlcRequest : FilterRequest
    {
        [Required]
        [FromQuery(Name = "asset_id")]
        public int AssetId { get; set; }
        
        [Required]
        [FromQuery(Name = "interval")]
        public string Interval { get; set; }
        
        [FromQuery(Name = "start_timestamp")]
        public DateTime? StartTimestamp { get; set; }

        [FromQuery(Name = "end_timestamp")]
        public DateTime? EndTimestamp { get; set; }
    }
}