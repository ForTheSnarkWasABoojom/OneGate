﻿using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Shared.ApiModels.User.Portfolio
{
    public class PortfolioFilterModel : FilterModel
    {
        [FromQuery(Name = "id")]
        public int? Id { get; set; }
    }
}