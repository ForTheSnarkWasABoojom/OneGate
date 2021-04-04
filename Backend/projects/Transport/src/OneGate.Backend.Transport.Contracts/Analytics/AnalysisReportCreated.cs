using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Contracts.Analytics
{
    [EntityName("event.analysis_report_created")]
    public class AnalysisReportCreated : BaseEvent
    {
        [JsonProperty("job_id")]
        public string JobId { get; set; }
        
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        
        [JsonProperty("buy_probability")]
        public string BuyProbability { get; set; }
        
        [JsonProperty("sell_probability")]
        public string SellProbability { get; set; }
        
        [JsonProperty("hold_probability")]
        public string HoldProbability { get; set; }
    }
}