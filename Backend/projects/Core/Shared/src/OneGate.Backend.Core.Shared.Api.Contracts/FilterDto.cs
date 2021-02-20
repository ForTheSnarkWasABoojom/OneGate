using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace OneGate.Backend.Core.Shared.Api.Contracts
{
    public class FilterDto
    {
        [DefaultValue(0)]
        [FromQuery(Name = "shift")]
        public int Shift { get; set; } = 0;
        
        [DefaultValue(1)]
        [FromQuery(Name = "count")]
        public int Count { get; set; } = 1;
    }
}