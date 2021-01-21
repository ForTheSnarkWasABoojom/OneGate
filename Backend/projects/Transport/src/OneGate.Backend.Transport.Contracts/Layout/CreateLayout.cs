using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Layout;

namespace OneGate.Backend.Transport.Contracts.Layout
{
    [EntityName("request.layout.create")]
    public class CreateLayout
    {
        public CreateLayoutDto Layout { get; set; }
    }
}