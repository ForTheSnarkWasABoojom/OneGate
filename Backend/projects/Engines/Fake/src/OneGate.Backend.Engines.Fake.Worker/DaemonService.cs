﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OneGate.Backend.Engines.Fake.Worker
{
    public class DaemonService : IHostedService
    {
        private readonly ILogger<DaemonService> _logger;

        public DaemonService(ILogger<DaemonService> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fake engine daemon service started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fake engine daemon service stopped");
        }
    }
}