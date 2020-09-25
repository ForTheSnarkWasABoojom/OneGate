using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Database.Models
{
    public class StopOrder:OrderBase
    {
        [Required]
        [Column("price")]
        public float Price { get; set; }
    }
}