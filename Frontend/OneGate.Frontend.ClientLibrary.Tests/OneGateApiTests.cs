using System.Threading.Tasks;
using Xunit;

namespace OneGate.Frontend.ClientLibrary.Tests
{
    public class OneGateApiTests : IClassFixture<OneGateApiFixture>
    {
        private readonly OneGateApiFixture _fixture;

        public OneGateApiTests(OneGateApiFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetMyAccountAsync()
        {
            await _fixture.UserApi.GetMyAccountAsync();
        }
    }
}