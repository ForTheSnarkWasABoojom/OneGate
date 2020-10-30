namespace OneGate.Backend.Contracts.Common
{
    public class SuccessResponse : ResponseBase
    {
        public SuccessResponse() : base(ResponseStatus.Success)
        {
        }
    }
}