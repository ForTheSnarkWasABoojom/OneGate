﻿using MassTransit.Topology;

namespace OneGate.Backend.Contracts.Layout
{
    [EntityName("request.layout.delete")]
    public class DeleteLayout
    {
        public int Id { get; set; }
    }
}