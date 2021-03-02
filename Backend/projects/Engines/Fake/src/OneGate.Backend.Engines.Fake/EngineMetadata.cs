using OneGate.Backend.Engines.Shared.Domain;

namespace OneGate.Backend.Engines.Fake
{
    public class EngineMetadata : IEngineMetadata
    {
        public int EngineId { get; } = 1;
    }
}