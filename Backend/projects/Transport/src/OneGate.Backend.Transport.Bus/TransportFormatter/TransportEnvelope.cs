using System;
using MassTransit;
using Newtonsoft.Json;

namespace OneGate.Backend.Transport.Bus.TransportFormatter
{
    public class TransportEnvelope
    {
        [JsonProperty("contract")]
        public string Contract { get; set; }

        [JsonProperty("payload")]
        public object Payload { get; set; }

        [JsonProperty("response_address")]
        public string ResponseAddress { get; set; }

        [JsonProperty("message_id")]
        public string MessageId { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("correlation_id")]
        public string CorrelationId { get; set; }

        [JsonProperty("conversation_id")]
        public string ConversationId { get; set; }

        [JsonProperty("initiator_id")]
        public string InitiatorId { get; set; }

        [JsonProperty("source_address")]
        public string SourceAddress { get; set; }

        [JsonProperty("destination_address")]
        public string DestinationAddress { get; set; }

        [JsonProperty("fault_address")]
        public string FaultAddress { get; set; }

        [JsonProperty("expiration_time")]
        public DateTime? ExpirationTime { get; set; }

        [JsonProperty("sent_time")]
        public DateTime? SentTime { get; set; }

        public static TransportEnvelope FromSendContext(SendContext context, object payload)
        {
            var result = new TransportEnvelope();

            if (context.MessageId.HasValue)
                result.MessageId = context.MessageId.Value.ToString();

            if (context.RequestId.HasValue)
                result.RequestId = context.RequestId.Value.ToString();

            if (context.CorrelationId.HasValue)
                result.CorrelationId = context.CorrelationId.Value.ToString();

            if (context.ConversationId.HasValue)
                result.ConversationId = context.ConversationId.Value.ToString();

            if (context.InitiatorId.HasValue)
                result.InitiatorId = context.InitiatorId.Value.ToString();

            if (context.SourceAddress != null)
                result.SourceAddress = context.SourceAddress.ToString();

            if (context.DestinationAddress != null)
                result.DestinationAddress = context.DestinationAddress.ToString();

            if (context.ResponseAddress != null)
                result.ResponseAddress = context.ResponseAddress.ToString();

            if (context.FaultAddress != null)
                result.FaultAddress = context.FaultAddress.ToString();

            if (context.TimeToLive.HasValue)
                result.ExpirationTime = DateTime.UtcNow + context.TimeToLive;

            result.SentTime = context.SentTime ?? DateTime.UtcNow;

            result.Payload = payload;

            result.Contract = TransportExtensions.GetEntityName(payload.GetType());

            return result;
        }
    }
}