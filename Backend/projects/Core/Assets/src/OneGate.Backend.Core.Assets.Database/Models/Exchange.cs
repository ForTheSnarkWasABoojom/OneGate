using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Assets.Database.Models
{
    [Table("exchange")]
    public class Exchange
    {
        [Key]
        [Column("id")]
        [Required]
        public int Id { get; set; }
        
        [MaxLength(50)]
        [Column("title")]
        [Required]
        public string Title { get; set; }
        
        [Column("engine_id")]
        [Required]
        public int EngineId { get; set; }

        [MaxLength(500)]
        [Column("description")]
        [Required]
        public string Description { get; set; }
        
        [MaxLength(150)]
        [Column("website")]
        public string Website { get; set; }
    }
}