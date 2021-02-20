using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MassTransit;
using MassTransit.Topology;
using Microsoft.Extensions.DependencyInjection;
using OneGate.Backend.Transport.Bus.Options;
using OneGate.Backend.Transport.Bus.TransportFormatter;

namespace OneGate.Backend.Transport.Bus
{
    public static class TransportExtensions
    {
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
            return services;
        }

        public static string GetEntityName(Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(EntityNameAttribute)).ToArray();
            return !attribute.Any() ? type.FullName : ((EntityNameAttribute) attribute.First()).EntityName;
        }
    }
}