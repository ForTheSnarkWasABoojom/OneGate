namespace OneGate.Backend.Contracts.Common
{
    public enum ResponseStatus
    {
        Success,
        Error
    }
    
    public abstract class ResponseBase
    {
        public ResponseStatus Status { get; }
        
        protected ResponseBase(ResponseStatus status)
        {
            Status = status;
        }
    }
}