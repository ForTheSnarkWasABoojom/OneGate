using MassTransit.Topology;
using OneGate.Common.Models.Layout;

namespace OneGate.Backend.Transport.Contracts.Layout
{
    [EntityName("request.layout.get")]
    public class GetLayouts
    {
        public LayoutFilterDto Filter { get; set; }
    }
}