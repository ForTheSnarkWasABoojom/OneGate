using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Shared.ApiModels.Layout
{
    public class CreateLayoutModel
    {
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