using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Asset.Database.Models
{
    [Table("asset_layout")]
    public class Layout
    {
        [Key]
        [Column("id")]
        [Required]
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