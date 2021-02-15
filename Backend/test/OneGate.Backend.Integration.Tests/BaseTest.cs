using System.IO;
using Microsoft.Extensions.Configuration;

namespace OneGate.Backend.Integration.Tests
{
    public class BaseTest
    {
        protected readonly IConfiguration Configuration;

        protected BaseTest()
        {
            Configuration = GetConfiguration();
        }

        private IConfiguration GetConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration;
        }
    }
}