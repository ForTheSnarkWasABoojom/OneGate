using System.Threading.Tasks;
using MassTransit;

namespace OneGate.Backend.Transport.Bus
{
    public interface IOgBus
    {
        public Task<TResponse> Call<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class;
        
        public Task<TResponse> Call<TRequest, TResponse>(TRequest request,
            RequestTimeout requestTimeout)
            where TRequest : class
            where TResponse : class;
    }
}