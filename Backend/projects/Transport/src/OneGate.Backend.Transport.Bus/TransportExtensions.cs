using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Topology;
using Microsoft.Extensions.DependencyInjection;
using OneGate.Backend.Transport.Bus.Options;
using OneGate.Backend.Transport.Bus.TransportFormatter;

namespace OneGate.Backend.Transport.Bus
{
    public static class TransportExtensions
    {
        public static async Task RespondFromMethod<TRequest, TResponse>(this ConsumeContext<TRequest> context,
            Func<TRequest, Task<TResponse>> action, IResponseExceptionHandler exceptionHandler)
            where TRequest : class
            where TResponse : class
        {
            try
            {
                var message = await action.Invoke(context.Message);
                await context.RespondAsync(message);
            }
            catch (Exception ex)
            {
                var errorResponse = exceptionHandler.CreateErrorResponse(ex);
                await context.RespondAsync(errorResponse);
            }
        }

        public static IServiceCollection UseTransportBus(this IServiceCollection services, RabbitMqOptions options,
            IEnumerable<KeyValuePair<Type, Type>> consumers = null)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(options.Host, "/", h =>
                    {
                        h.Username(options.Username);
                        h.Password(options.Password);
                    });
                    cfg.SetMessageSerializer(() => new TransportMessageSerializer());
                    cfg.AddMessageDeserializer(TransportMessageDeserializer.ContentTypeValue,
                        () => new TransportMessageDeserializer(TransportMessageSerializer.DeserializerInstance));
                    cfg.ConfigureEndpoints(context);
                });

                if (consumers == null) return;
                foreach (var (consumerType, consumerDefinition) in consumers)
                {
                    x.AddConsumer(consumerType, consumerDefinition);
                }
            });
            services.AddMassTransitHostedService();
            services.AddTransient<ITransportBus, TransportBus>();

            return services;
        }

        public static string GetEntityName(Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(EntityNameAttribute)).ToArray();
            return !attribute.Any() ? type.FullName : ((EntityNameAttribute) attribute.First()).EntityName;
        }
    }
}