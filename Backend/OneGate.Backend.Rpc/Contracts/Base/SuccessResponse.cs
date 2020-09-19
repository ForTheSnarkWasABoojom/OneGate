﻿namespace OneGate.Backend.Rpc.Contracts.Base
{
    public class SuccessResponse : ResponseBase
    {
        public SuccessResponse() : base(ResponseStatus.Success)
        {
        }
    }
}