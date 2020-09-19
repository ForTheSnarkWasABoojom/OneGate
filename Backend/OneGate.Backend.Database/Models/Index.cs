using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Database.Models
{
    [Table("asset")]
    public class Index : AssetBase
    {
        [MaxLength(100)]
        [Column("country")]
        public string Country { get; set; }
    }
}