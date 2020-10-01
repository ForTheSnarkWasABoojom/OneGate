using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Database.Models
{
    [Table("account")]
    public class Account
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        
        [Column("first_name")]
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        
        [Column("last_name")]
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Column("email")]
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        
        [Column("password")]
        [Required]
        public string Password { get; set; }
        
        [Column("is_admin")]
        [Required]
        public bool IsAdmin { get; set; }
    }
}