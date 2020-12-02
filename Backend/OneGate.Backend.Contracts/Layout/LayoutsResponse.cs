using System.Collections.Generic;
using MassTransit.Topology;
using OneGate.Shared.Models.Layout;

namespace OneGate.Backend.Contracts.Layout
{
    [EntityName("response.layout")]
    public class LayoutsResponse
    {
        public IEnumerable<LayoutDto> Layouts { get; set; }
    }
}