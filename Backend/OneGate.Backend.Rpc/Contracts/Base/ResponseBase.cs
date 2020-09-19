﻿namespace OneGate.Backend.Rpc.Contracts.Base
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