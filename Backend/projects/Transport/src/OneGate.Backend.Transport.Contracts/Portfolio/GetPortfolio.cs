﻿using MassTransit.Topology;
using OneGate.Common.Models.Portfolio;

namespace OneGate.Backend.Transport.Contracts.Portfolio
{
    [EntityName("request.portfolio.get")]
    public class GetPortfolios
    {
        public PortfolioFilterDto Filter { get; set; }
        public int OwnerId { get; set; }
    }
}