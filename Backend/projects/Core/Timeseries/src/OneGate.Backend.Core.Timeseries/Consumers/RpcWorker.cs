using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Core.Timeseries.Contracts.Series;
using OneGate.Backend.Core.Timeseries.Services;
using OneGate.Backend.Transport.Bus;

namespace OneGate.Backend.Core.Timeseries.Consumers
{
    public class RpcWorker :
        IConsumer<GetSeries>
    {
        private readonly ISeriesService _service;

        private readonly IResponseExceptionHandler _exceptionHandler;

        public RpcWorker(ISeriesService service, IResponseExceptionHandler exceptionHandler)
        {
            _service = service;
            _exceptionHandler = exceptionHandler;
        }

        public async Task Consume(ConsumeContext<GetSeries> context)
        {
            await context.RespondFromMethod(_service.GetSeriesAsync, _exceptionHandler);
        }
    }
}