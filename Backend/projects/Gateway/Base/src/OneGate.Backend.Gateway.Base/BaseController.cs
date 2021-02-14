using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneGate.Shared.ApiModels.Base;

namespace OneGate.Backend.Gateway.Base
{
    [ApiController]
    [Authorize]
    [ProducesResponseType(typeof(ErrorModel),StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorModel),StatusCodes.Status500InternalServerError)]
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