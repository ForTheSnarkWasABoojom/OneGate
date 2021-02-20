using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Core.Shared.Api.Contracts;

namespace OneGate.Backend.Core.Shared.Api
{
    [ApiController]
    [ProducesResponseType(typeof(ErrorDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto),StatusCodes.Status500InternalServerError)]
    public abstract class BaseController : ControllerBase
    {
        protected const string RouteBase = "api/v{version:apiVersion}/";
    }
}