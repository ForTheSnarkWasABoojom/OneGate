using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Core.Shared.Api;
using OneGate.Backend.Core.Shared.Database.Repository;
using OneGate.Backend.Core.Shared.Linq;
using OneGate.Backend.Core.Users.Api.Contracts.Administrator;
using OneGate.Backend.Core.Users.Database.Models;
using OneGate.Backend.Core.Users.Database.Repository;

namespace OneGate.Backend.Core.Users.Api.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "administrators")]
    public class AdministratorsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AdministratorsController> _logger;
        private readonly IAdministratorRepository _administrators;

        public AdministratorsController(ILogger<AdministratorsController> logger, IMapper mapper, IAdministratorRepository administrators)
        {
            _logger = logger;
            _mapper = mapper;
            _administrators = administrators;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdministratorAsync([FromBody] CreateAdministratorDto request)
        {
            var administrator = _mapper.Map<Administrator>(request);
            await _administrators.AddAsync(administrator);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAdministratorsAsync([FromQuery] FilterAdministratorsDto request)
        {
            Expression<Func<Administrator, bool>> filter = p => true;
            var limits = new QueryLimits(request.Shift, request.Count);

            filter = filter
                .FilterBy(p => p.Id == request.Id, request.Id)
                .FilterBy(p => p.Email == request.Email, request.Email)
                .FilterBy(p => p.Password == request.Password, request.Password);

            var accounts = await _administrators.FilterAsync(filter, limits: limits);

            var accountsDto = _mapper.Map<IEnumerable<AdministratorDto>>(accounts);
            return Ok(accountsDto);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAdministratorAsync([FromRoute] int id)
        {
            await _administrators.RemoveAsync(p =>
                p.Id == id
            );

            return Ok();
        }
    }
}