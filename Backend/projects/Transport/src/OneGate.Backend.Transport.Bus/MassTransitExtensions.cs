using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EntityFramework.Exceptions.Common;
using MassTransit;
using MassTransit.Topology;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OneGate.Backend.Transport.Bus.OgFormatter;
using OneGate.Backend.Transport.Contracts.Common;

namespace OneGate.Backend.Transport.Bus
{
    public static class MassTransitExtensions
    {
        public static async Task MarshallWith<TRequest, TResponse>(this ConsumeContext<TRequest> context, Func<TRequest, Task<TResponse>> action)
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
                await context.RespondAsync(FromException(ex));
            }
        }
        
        private static ErrorResponse FromException(Exception exception)
        {
            return exception switch
            {
                ApiException ex => new ErrorResponse
                {
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                    InnerExceptionMessage = ex.InnerExceptionMessage
                },
                UniqueConstraintException ex => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "Entity must be unique",
                    InnerExceptionMessage = ex.Message
                },
                ReferenceConstraintException ex => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status424FailedDependency,
                    Message = "Entity has wrong dependencies",
                    InnerExceptionMessage = ex.Message
                },
                { } ex => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    InnerExceptionMessage = ex.ToString()
                }
            };
        }

        public static IServiceCollection UseMassTransit(this IServiceCollection services, IEnumerable<KeyValuePair<Type, Type>> consumers = null)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq", "/", h =>
                    {
                        h.Username(Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER"));
                        h.Password(Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS"));
                    });
                    cfg.SetMessageSerializer(() => new OgMessageSerializer());
                    cfg.AddMessageDeserializer(OgMessageDeserializer.ContentTypeValue,
                        () => new OgMessageDeserializer(OgMessageSerializer.DeserializerInstance));
                    cfg.ConfigureEndpoints(context);
                });

                if (consumers == null) return;
                foreach (var (consumerType, consumerDefinition) in consumers)
                {
                    x.AddConsumer(consumerType, consumerDefinition);
                }
            });
            services.AddMassTransitHostedService();
            services.AddTransient<IOgBus, OgBus>();
            
            return services;
        }
        
        public static string GetEntityName(Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(EntityNameAttribute)).ToArray();
            return !attribute.Any() ? type.FullName : ((EntityNameAttribute) attribute.First()).EntityName;
        }
    }
}