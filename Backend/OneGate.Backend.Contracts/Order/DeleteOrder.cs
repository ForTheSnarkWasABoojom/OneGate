﻿using MassTransit.Topology;

namespace OneGate.Backend.Contracts.Order
{
    [EntityName("order.delete")]
    public class DeleteOrder
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
    }
}