using System.Threading.Tasks;
using MassTransit;

namespace OneGate.Backend.Transport.Bus
{
    public interface ITransportBus
    {
        public Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class;

        public Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request,
            RequestTimeout requestTimeout)
            where TRequest : class
            where TResponse : class;
    }
}