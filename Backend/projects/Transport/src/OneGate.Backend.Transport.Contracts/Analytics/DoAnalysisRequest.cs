using MassTransit.Topology;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Contracts.Analytics
{
    [EntityName("request.do_analysis")]
    public class DoAnalysisRequest
    {
        [JsonProperty("job")]
        public AnalyticJobDto Job { get; set; }
        
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }
}