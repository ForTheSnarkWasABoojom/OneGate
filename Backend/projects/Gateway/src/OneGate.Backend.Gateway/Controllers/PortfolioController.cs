﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Extensions;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Portfolio;
using OneGate.Common.Models.Common;
using OneGate.Common.Models.Portfolio;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace OneGate.Backend.Gateway.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto), Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ErrorDto), Status400BadRequest)]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly ILogger<PortfolioController> _logger;
        private readonly IOgBus _bus;

        public PortfolioController(ILogger<PortfolioController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResourceDto), Status200OK)]
        [SwaggerOperation("Create portfolio")]
        public async Task<ResourceDto> CreatePortfolioAsync([FromBody] CreatePortfolioDto request)
        {
            var payload = await _bus.Call<CreatePortfolio, CreatedResourceResponse>(new CreatePortfolio
            {
                Portfolio = request,
                OwnerId = User.GetAccountId()
            });

            return payload.Resource;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(PortfolioDto), Status200OK)]
        [SwaggerOperation("Portfolio details")]
        [Route("{id}")]
        public async Task<PortfolioDto> GetPortfolioAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetPortfolios, PortfoliosResponse>(new GetPortfolios
            {
                Filter = new PortfolioFilterDto
                {
                    Id = id
                }
            });

            return payload.Portfolios.First();
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PortfolioDto>), Status200OK)]
        [SwaggerOperation("Search portfolios")]
        public async Task<IEnumerable<PortfolioDto>> GetPortfoliosRangeAsync([FromQuery] PortfolioFilterDto request)
        {
            var payload = await _bus.Call<GetPortfolios, PortfoliosResponse>(new GetPortfolios
            {
                Filter = request,
                OwnerId = User.GetAccountId()
            });

            return payload.Portfolios;
        }

        [HttpDelete]
        [SwaggerOperation("Delete portfolio")]
        [Route("{id}")]
        public async Task DeletePortfolioAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<DeletePortfolio, SuccessResponse>(new DeletePortfolio
            {
                Id = id,
                OwnerId = User.GetAccountId()
            });
        }
    }
}