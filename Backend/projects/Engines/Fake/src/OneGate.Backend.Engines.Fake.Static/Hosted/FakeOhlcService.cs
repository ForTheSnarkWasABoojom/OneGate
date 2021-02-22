using OneGate.Backend.Core.Engines.Api.Client;
using OneGate.Backend.Engines.Shared;

namespace OneGate.Backend.Engines.Fake.Static.Hosted
{
    public class FakeOhlcService : OhlcService
    {
        public override int EngineId { get; } = 1;

        public FakeOhlcService(IEnginesApiClient enginesApi) : base(enginesApi)
        {
        }
    }
}