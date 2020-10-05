using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OneGate.Backend.Database;
using OneGate.Backend.Rpc;

namespace OneGate.Backend.Engines.FakeStaticEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<DaemonService>();

                    services.AddSingleton<IBus>(provider => BusFactory.GetInstance());

                    services.AddEntityFrameworkNpgsql().AddDbContext<DatabaseContext>();
                });
    }
}