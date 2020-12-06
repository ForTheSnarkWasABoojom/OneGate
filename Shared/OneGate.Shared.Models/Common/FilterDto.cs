using System.ComponentModel;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace OneGate.Shared.Models.Common
{
    public class FilterDto
    {
        [DefaultValue(0)]
        [FromQuery(Name = "shift")]
        [JsonProperty("shift")]
        public int Shift { get; set; } = 0;
        
        [DefaultValue(1)]
        [FromQuery(Name = "count")]
        [JsonProperty("count")]
        public int Count { get; set; } = 1;
    }
}