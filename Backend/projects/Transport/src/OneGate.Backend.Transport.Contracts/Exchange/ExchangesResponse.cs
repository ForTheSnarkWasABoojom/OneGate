using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Exchange;

namespace OneGate.Backend.Transport.Contracts.Exchange
{
    [EntityName("response.exchanges")]
    public class ExchangesResponse
    {
        public IEnumerable<ExchangeDto> Exchanges { get; set; }
    }
}