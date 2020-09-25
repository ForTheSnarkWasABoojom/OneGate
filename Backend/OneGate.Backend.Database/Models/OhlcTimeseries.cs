using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Database.Models
{
    [Table("ohlc_timeseries")]
    public class OhlcTimeseries : TimeseriesBase
    {
        [Required]
        [MaxLength(4)]
        [Column("interval")]
        public string Interval { get; set; }
        
        [Required]
        [Column("low")]
        public float Low { get; set; }

        [Required] 
        [Column("high")]
        public float High { get; set; }

        [Required]
        [Column("open")]
        public float Open { get; set; }

        [Required]
        [Column("close")]
        public float Close { get; set; }
    }
}