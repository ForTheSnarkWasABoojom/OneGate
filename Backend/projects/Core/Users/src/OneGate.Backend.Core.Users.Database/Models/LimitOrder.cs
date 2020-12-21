using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Users.Database.Models
{
    [Table("order")]
    public class LimitOrder : Order
    {
        [Required] 
        [Column("price")]
        public float Price { get; set; }
    }
}