using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Assets.Database.Models
{
    [Table("asset")]
    public class IndexAsset : Asset
    {
        [MaxLength(100)]
        [Column("country")]
        public string Country { get; set; }
    }
}