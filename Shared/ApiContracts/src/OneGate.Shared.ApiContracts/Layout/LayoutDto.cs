using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace OneGate.Shared.ApiContracts.Layout
{
    public class LayoutDto
    {
        [Required]
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [Required] 
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }
        
        [MaxLength(500)]
        [Column("description")]
        [Required]
        public string Description { get; set; }
    }
}