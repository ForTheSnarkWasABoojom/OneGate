using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Timeseries.Api.Contracts.Artifact
{
    public class FilterArtifactDto : FilterDto
    {
        [Required]
        [FromQuery(Name = "layer_id")]
        public int LayerId { get; set; }

        [FromQuery(Name = "start_timestamp")]
        public DateTime? StartTimestamp { get; set; }

        [FromQuery(Name = "end_timestamp")]
        public DateTime? EndTimestamp { get; set; }
    }
}