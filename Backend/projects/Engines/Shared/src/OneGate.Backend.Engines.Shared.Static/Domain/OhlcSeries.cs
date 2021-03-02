using System;

namespace OneGate.Backend.Engines.Shared.Static.Domain
{
    public class OhlcSeries
    {
        public float Open { get; set; }
        
        public float High { get; set; }
        
        public float Low { get; set; }
        
        public float Close { get; set; }
        
        public DateTime Timestamp { get; set; }
        
        public SeriesInterval Interval { get; set; }
    }
}