using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Database.Models
{
    [Table("portfolio")]
    public class Portfolio
    {
        [Key] 
        [Column("id")] 
        [Required] 
        public int Id { get; set; }

        [MaxLength(50)]
        [Column("name")]
        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        [Column("description")]
        public string Description { get; set; }

        [Column("owner_id")]
        [Required]
        public int OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))] 
        public Account Owner { get; set; }
    }
}