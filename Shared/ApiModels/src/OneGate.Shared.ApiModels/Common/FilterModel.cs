using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Common
{
    public class FilterModel
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