using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiModels.Layout
{
    public class LayoutModel
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [Required] 
        [MaxLength(100)]
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [MaxLength(500)]
        [JsonProperty("description")]
        [Required]
        public string Description { get; set; }
    }
}