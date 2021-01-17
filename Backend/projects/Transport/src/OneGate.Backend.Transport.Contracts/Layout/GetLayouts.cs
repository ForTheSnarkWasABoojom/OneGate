using MassTransit.Topology;
using OneGate.Shared.ApiContracts.Layout;

namespace OneGate.Backend.Transport.Contracts.Layout
{
    [EntityName("request.layout.get")]
    public class GetLayouts
    {
        public LayoutFilterDto Filter { get; set; }
    }
}