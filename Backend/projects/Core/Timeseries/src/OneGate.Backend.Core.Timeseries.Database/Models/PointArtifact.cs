using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Timeseries.Database.Models
{
    [Table("artifact")]
    public class PointArtifact : Artifact
    {
        [Required] 
        [Column("value")]
        public float Value { get; set; }
    }
}