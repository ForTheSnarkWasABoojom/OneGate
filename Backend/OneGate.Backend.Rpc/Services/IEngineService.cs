using EasyNetQ;

namespace OneGate.Backend.Rpc.Services
{
    public interface IEngineService
    {
        
    }

    public class EngineService : IEngineService
    {
        private IBus _bus;

        public EngineService(IBus bus)
        {
            _bus = bus;
        }
    }
}