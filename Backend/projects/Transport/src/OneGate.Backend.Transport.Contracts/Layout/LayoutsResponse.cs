using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Layout;

namespace OneGate.Backend.Transport.Contracts.Layout
{
    [EntityName("response.layout")]
    public class LayoutsResponse
    {
        public IEnumerable<LayoutDto> Layouts { get; set; }
    }
}