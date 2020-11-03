using MassTransit.Topology;
using OneGate.Shared.Models.Common;

namespace OneGate.Backend.Contracts.Common
{
    [EntityName("response.created_resource")]
    public class CreatedResourceResponse : SuccessResponse
    {
        public ResourceDto Resource { get; set; }
    }
}