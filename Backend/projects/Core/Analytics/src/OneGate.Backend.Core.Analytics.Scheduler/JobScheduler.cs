using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace OneGate.Backend.Core.Analytics.Scheduler
{
    public class JobScheduler : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
        }
    }
}