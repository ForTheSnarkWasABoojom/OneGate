using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.User.Database.Models
{
    [Table("order")]
    public class LimitOrder : OrderBase
    {
        [Required] 
        [Column("price")]
        public float Price { get; set; }
    }
}