using MassTransit.Topology;
using OneGate.Shared.Models.Layout;

namespace OneGate.Backend.Contracts.Layout
{
    [EntityName("request.layout.get")]
    public class GetLayouts
    {
        public LayoutFilterDto Filter { get; set; }
    }
}