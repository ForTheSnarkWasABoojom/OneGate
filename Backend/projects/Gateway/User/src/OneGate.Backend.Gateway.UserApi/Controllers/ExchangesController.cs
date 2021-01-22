﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Transport.Dto.Exchange;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Exchange;
using OneGate.Shared.ApiModels.Exchange;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.UserApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "exchanges")]
    public class ExchangesController : BaseController
    {
        private readonly ILogger<ExchangesController> _logger;
        
        private readonly IMapper _mapper;
        private readonly IOgBus _bus;

        public ExchangesController(ILogger<ExchangesController> logger, IOgBus bus, IMapper mapper)
        {
            _logger = logger;
            _bus = bus;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ExchangeModel>), StatusCodes.Status200OK)]
        [SwaggerOperation("Get exchanges by specified filter")]
        public async Task<IActionResult> GetExchangesRangeAsync([FromQuery] ExchangeFilterModel request)
        {
            var exchangeFilterDto = _mapper.Map<ExchangeFilterModel, ExchangeFilterDto>(request);
            var payload = await _bus.Call<GetExchanges, ExchangesResponse>(new GetExchanges
            {
                Filter = exchangeFilterDto
            });

            var response = _mapper.Map<IEnumerable<ExchangeDto>, IEnumerable<ExchangeModel>>(payload.Exchanges);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ExchangeModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation("Exchange details")]
        [Route("{id}")]
        public async Task<IActionResult> GetExchangeAsync([FromRoute] int id)
        {
            var payload = await _bus.Call<GetExchanges, ExchangesResponse>(new GetExchanges
            {
                Filter = new ExchangeFilterDto
                {
                    Id = id
                }
            });
            
            var response = _mapper.Map<ExchangeDto, ExchangeModel>(payload.Exchanges.FirstOrDefault());
            return StrictOk(response);
        }
    }
}