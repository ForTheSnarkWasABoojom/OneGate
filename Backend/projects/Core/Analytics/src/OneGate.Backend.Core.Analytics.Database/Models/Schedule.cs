using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Analytics.Database.Models
{
    [Table("schedule")]
    public class Schedule
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("job_id")]
        public int JobId { get; set; }
        
        [Column("period_sec")]
        [Required]
        public long PeriodSec { get; set; }
                
        [ForeignKey(nameof(JobId))]
        public Job Job { get; set; }
    }
}