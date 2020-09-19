using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Database.Models
{
    [Table("asset")]
    public class Stock : AssetBase
    {
        [MaxLength(100)]
        [Column("company")]
        public string Company { get; set; }
    }
}