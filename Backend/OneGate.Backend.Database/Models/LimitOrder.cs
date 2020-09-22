using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Database.Models
{
    public class LimitOrder:OrderBase
    {
        [Column("price")]
        public float Price { get; set; }
    }
}