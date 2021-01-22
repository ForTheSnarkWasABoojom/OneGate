using Ploeh.AutoFixture;
using Xunit;

namespace OneGate.Backend.Integration.Tests
{
    public class IntegrationTests : IClassFixture<ApiFixture>
    {
        private readonly ApiFixture _apiFixture;
        private readonly Fixture _fixture;

        public IntegrationTests(ApiFixture fixture)
        {
            _apiFixture = fixture;
        }
    }
}