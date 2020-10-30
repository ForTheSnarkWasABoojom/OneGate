using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Backend.Contracts.Timeseries;
using OneGate.Backend.Rpc;
using OneGate.Backend.Rpc.Services;
using OneGate.Backend.Services.TimeseriesService.Repository;
using OneGate.Shared.Models.Timeseries;

namespace OneGate.Backend.Services.TimeseriesService
{
    public class Consumer :
        IConsumer<OnOhlcTimeseriesUpdated>,
        IConsumer<CreateOhlcTimeseriesRange>,
        IConsumer<GetOhlcTimeseriesRange>,
        IConsumer<DeleteOhlcTimeseriesRange>,
        IConsumer<CreateValueTimeseriesRange>,
        IConsumer<GetValueTimeseriesRange>,
        IConsumer<DeleteValueTimeseriesRange>
    {
        private readonly ITimeseriesService _service;

        public Consumer(ITimeseriesService service)
        {
            _service = service;
        }

        public async Task Consume(ConsumeContext<OnOhlcTimeseriesUpdated> context)
        {
            await _service.OnOhlcTimeseriesUpdated(context.Message);
        }

        public async Task Consume(ConsumeContext<CreateOhlcTimeseriesRange> context)
        {
            await context.MarshallWith(_service.CreateOhlcTimeseriesRange);
        }

        public async Task Consume(ConsumeContext<GetOhlcTimeseriesRange> context)
        {
            await context.MarshallWith(_service.GetOhlcTimeseriessRange);
        }

        public async Task Consume(ConsumeContext<DeleteOhlcTimeseriesRange> context)
        {
            await context.MarshallWith(_service.DeleteOhlcTimeseriesRange);
        }

        public async Task Consume(ConsumeContext<CreateValueTimeseriesRange> context)
        {
            await context.MarshallWith(_service.CreateValueTimeseriesRange);
        }

        public async Task Consume(ConsumeContext<GetValueTimeseriesRange> context)
        {
            await context.MarshallWith(_service.GetValueTimeseriessRange);
        }

        public async Task Consume(ConsumeContext<DeleteValueTimeseriesRange> context)
        {
            await context.MarshallWith(_service.DeleteValueTimeseriesRange);
        }
    }
}