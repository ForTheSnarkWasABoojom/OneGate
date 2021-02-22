using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Users.Database.Models
{
    [Table("administrator")]
    public class Administrator
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("email")]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Column("password")]
        [Required]
        public string Password { get; set; }
    }
}