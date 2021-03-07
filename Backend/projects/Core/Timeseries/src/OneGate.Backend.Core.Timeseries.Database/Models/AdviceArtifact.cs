using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Timeseries.Database.Models
{
    [Table("artifact")]
    public class AdviceArtifact : Artifact
    {
        [Required] 
        [Column("buy_probability")]
        public float BuyProbability { get; set; }
        
        [Required] 
        [Column("sell_probability")]
        public float SellProbability { get; set; }
        
        [Required] 
        [Column("hold_probability")]
        public float HoldProbability { get; set; }
    }
}