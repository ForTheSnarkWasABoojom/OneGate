using MassTransit.Topology;
using OneGate.Shared.Models.Layout;

namespace OneGate.Backend.Contracts.Layout
{
    [EntityName("request.layout.create")]
    public class CreateLayout
    {
        public CreateLayoutDto Layout { get; set; }
    }
}