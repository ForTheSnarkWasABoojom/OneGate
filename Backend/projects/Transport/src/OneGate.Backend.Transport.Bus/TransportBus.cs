using System.Threading.Tasks;
using MassTransit;
using OneGate.Backend.Transport.Bus.Contracts;
using OneGate.Backend.Transport.Bus.Exceptions;

namespace OneGate.Backend.Transport.Bus
{
    public class TransportBus : ITransportBus
    {
        private readonly IBus _bus;

        public TransportBus(IBus bus)
        {
            _bus = bus;
        }

        public async Task<TResponse> Call<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class
        {
            return await Call<TRequest, TResponse>(request, default);
        }

        public async Task<TResponse> Call<TRequest, TResponse>(TRequest request, RequestTimeout requestTimeout)
            where TRequest : class
            where TResponse : class
        {
            var client = _bus.CreateRequestClient<TRequest>();
            var (message, error) = await client.GetResponse<TResponse, ErrorResponse>(request, timeout: requestTimeout);

            if (!error.IsCompletedSuccessfully)
                return (await message).Message;

            var errorResponse = (await error).Message;
            throw new RemoteException(errorResponse.Message, errorResponse.StatusCode,
                errorResponse.InnerExceptionMessage);
        }
    }
}