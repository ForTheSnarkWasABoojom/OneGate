using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneGate.Common.Models.Common;

namespace OneGate.Backend.Gateway.Base
{
    [ApiController]
    [Authorize]
    [ProducesResponseType(typeof(ErrorDto),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto),StatusCodes.Status500InternalServerError)]
    public abstract class BaseController : ControllerBase
    {
        protected const string RouteBase = "api/v{version:apiVersion}/";
    }
}