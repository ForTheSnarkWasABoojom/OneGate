using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Assets.Database.Models
{
    [Table("asset")]
    public class StockAsset : Asset
    {
        [MaxLength(100)]
        [Column("company")]
        public string Company { get; set; }
    }
}