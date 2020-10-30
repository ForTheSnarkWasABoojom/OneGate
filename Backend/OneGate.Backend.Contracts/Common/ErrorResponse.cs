namespace OneGate.Backend.Contracts.Common
{
    public class ErrorResponse : ResponseBase
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string InnerExceptionMessage { get; set; }
        public ErrorResponse() : base(ResponseStatus.Error)
        {
        }
    }
}