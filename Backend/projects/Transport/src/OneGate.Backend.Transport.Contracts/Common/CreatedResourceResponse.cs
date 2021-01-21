using MassTransit.Topology;
using OneGate.Backend.Transport.Dto.Common;

namespace OneGate.Backend.Transport.Contracts.Common
{
    [EntityName("response.created_resource")]
    public class CreatedResourceResponse : SuccessResponse
    {
        public ResourceDto Resource { get; set; }
    }
}