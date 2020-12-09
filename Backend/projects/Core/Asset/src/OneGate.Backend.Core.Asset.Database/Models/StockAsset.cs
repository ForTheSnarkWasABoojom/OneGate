using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Asset.Database.Models
{
    [Table("asset")]
    public class StockAsset : AssetBase
    {
        [MaxLength(100)]
        [Column("company")]
        public string Company { get; set; }
    }
}