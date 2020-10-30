using OneGate.Shared.Models.Common;

namespace OneGate.Backend.Contracts.Common
{
    public class CreatedResourceResponse : SuccessResponse
    {
        public ResourceDto Resource { get; set; }
    }
}