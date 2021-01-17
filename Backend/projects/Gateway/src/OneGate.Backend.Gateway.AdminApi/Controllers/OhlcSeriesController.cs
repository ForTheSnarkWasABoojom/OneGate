using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Gateway.Base;
using OneGate.Backend.Transport.Bus;
using OneGate.Backend.Transport.Contracts.Common;
using OneGate.Backend.Transport.Contracts.Series.Ohlc;
using OneGate.Shared.ApiContracts.Series.Ohlc;
using Swashbuckle.AspNetCore.Annotations;

namespace OneGate.Backend.Gateway.AdminApi.Controllers
{
    [ApiVersion("1")]
    [Route(RouteBase + "ohlc_series")]
    public class OhlcSeriesController : BaseController
    {
        private readonly ILogger<OhlcSeriesController> _logger;
        private readonly IOgBus _bus;

        public OhlcSeriesController(ILogger<OhlcSeriesController> logger, IOgBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost]
        [SwaggerOperation("Create OHLC timeseries range")]
        public async Task CreateOhlcSeriesAsync([FromBody] OhlcSeriesDto request)
        {
            await _bus.Call<CreateOhlcSeries, SuccessResponse>(
                new CreateOhlcSeries
                {
                    Series = request
                });
        }

        [HttpDelete]
        [SwaggerOperation("Delete OHLC timeseries range")]
        public async Task DeleteOhlcSeriesAsync([FromQuery] OhlcSeriesFilterDto request)
        {
            await _bus.Call<DeleteOhlcSeries, SuccessResponse>(new DeleteOhlcSeries
            {
                Filter = request
            });
        }
    }
}