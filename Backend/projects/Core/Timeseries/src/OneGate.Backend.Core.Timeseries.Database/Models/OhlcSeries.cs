﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneGate.Backend.Core.Timeseries.Database.Models
{
    [Table("ohlc_series")]
    public class OhlcSeries : Series
    {
        [Required]
        [MaxLength(4)]
        [Column("interval")]
        public string Interval { get; set; }
        
        [Required]
        [Column("low")]
        public double Low { get; set; }

        [Required] 
        [Column("high")]
        public double High { get; set; }

        [Required]
        [Column("open")]
        public double Open { get; set; }

        [Required]
        [Column("close")]
        public double Close { get; set; }
    }
}