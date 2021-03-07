using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Ohlc
{
    public class FilterOhlcDto : FilterDto
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