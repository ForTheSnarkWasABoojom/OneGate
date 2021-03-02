using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneGate.Backend.Gateway.Shared.Api.Contracts;

namespace OneGate.Backend.Gateway.Shared.Api
{
    [ApiController]
    [Authorize]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse),StatusCodes.Status500InternalServerError)]
    public abstract class BaseController : ControllerBase
    {
        protected const string RouteBase = "api/v{version:apiVersion}/";

        /// <summary>
        /// Returns <see cref="StatusCodes.Status404NotFound"/> if passed object is null,
        /// otherwise <see cref="StatusCodes.Status200OK"/>.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        protected IActionResult StrictOk(object response)
        {
            if (response is null)
                return NotFound();
            return Ok(response);
        }
    }
}