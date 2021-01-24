using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.AdminApi.Converters;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Shared.ApiModels.Series.Ohlc;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "ohlc_series")]
    public class OhlcSeriesController : BaseController
    {
        private readonly ILogger<OhlcSeriesController> _logger;
        private readonly IConverter _converter;
        private readonly IOgBus _bus;

        public OhlcSeriesController(ILogger<OhlcSeriesController> logger, IOgBus bus, IConverter converter)
        {
            _logger = logger;
            _bus = bus;
            _converter = converter;
        }

        [HttpPost]
        [SwaggerOperation("Create OHLC timeseries range")]
        public async Task<IActionResult> CreateOhlcSeriesAsync([FromBody] OhlcSeriesModel request)
        {
            var createdOhlcDto = _converter.ToDto(request);
            await _bus.Call<CreateOhlcSeries, SuccessResponse>(
                new CreateOhlcSeries
                {
                    Series = createdOhlcDto
                });

            return Ok();
        }

        [HttpDelete]
        [SwaggerOperation("Delete OHLC timeseries range")]
        public async Task<IActionResult> DeleteOhlcSeriesAsync([FromQuery] OhlcSeriesFilterModel request)
        {
            var ohlcSeriesFilterDto = _converter.ToDto(request);
            await _bus.Call<DeleteOhlcSeries, SuccessResponse>(new DeleteOhlcSeries
            {
                Filter = ohlcSeriesFilterDto
            });

            return Ok();
        }
    }
}