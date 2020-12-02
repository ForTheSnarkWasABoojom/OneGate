using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.Models.Exchange;

namespace OneGate.Backend.Contracts.Exchange
{
    [EntityName("response.exchanges")]
    public class ExchangesResponse
    {
        public IEnumerable<ExchangeDto> Exchanges { get; set; }
    }
}