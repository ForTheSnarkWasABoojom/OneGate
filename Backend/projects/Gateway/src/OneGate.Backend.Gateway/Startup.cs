using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OneGate.Backend.Database;
using OneGate.Backend.Database.Models;
using OneGate.Backend.Gateway.Consumers;
using OneGate.Backend.Gateway.EventHubs;
using OneGate.Backend.Gateway.Extensions;
using OneGate.Backend.Gateway.Middleware;
using OneGate.Backend.Transport.Bus;
using OneGate.Common.Models.Exchange;
using Prometheus;

namespace OneGate.Backend.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();

            // Custom validation and authorization by default.
            services.AddMvc(options => { options.Filters.Add(typeof(ValidateModelAttribute)); });

            // Versioning.
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
            });

            // Authentication.
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = GroupPolicies.SecurityKey
                    };
                });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(GroupPolicies.Admin, GroupPolicies.AdminPolicy());
                config.AddPolicy(GroupPolicies.User, GroupPolicies.UserPolicy());
            });
            services.AddTransient<IAuthCredentials, AuthCredentials>();

            // Suppress default filter.
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            // Enforce lowercase for routers.
            services.AddRouting(options => options.LowercaseUrls = true);

            // Swagger.
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "OneGate",
                    Version = "v1"
                });
                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                options.EnableAnnotations(enableAnnotationsForInheritance: true,
                    enableAnnotationsForPolymorphism: true);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();

            // Migration.
            services.AddDbContext<DatabaseContext>();
            
            // Mass Transit.
            services.UseMassTransit(new []
            {
                new KeyValuePair<Type, Type>(typeof(AccountConsumer), typeof(AccountConsumerSettings)),
                new KeyValuePair<Type, Type>(typeof(TimeseriesConsumer), typeof(TimeseriesConsumerSettings)),
            });

            // Event hub.
            services.AddSignalR();
        }

        private static void MigrateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var db = serviceScope.ServiceProvider.GetService<DatabaseContext>();

            Thread.Sleep(3000);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var account = new Account
            {
                FirstName = "ONEGATE",
                LastName = "ADMINISTRATOR",
                Email = Environment.GetEnvironmentVariable("API_ADMIN_EMAIL"),
                Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: Environment.GetEnvironmentVariable("API_ADMIN_PASSWORD"),
                    salt: new byte[128 / 8],
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8)),
                IsAdmin = true
            };
            
            db.Accounts.Add(account);
            
            var fakeExchange = new Exchange
            {
                Title = "FAKE",
                Description = "Fake exchange",
                EngineType = EngineTypeDto.FAKE.ToString()
            };
            
            var fakeIndex = new IndexAsset
            {
                Ticker = "OG",
                Country = "Russia",
                Exchange = fakeExchange,
                Description = "Fake index"
            };

            db.Exchanges.Add(fakeExchange);
            db.Assets.Add(fakeIndex);
            
            db.SaveChanges();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Database migration.
            MigrateDatabase(app);

            // Custom exception handler.
            app.UseExceptionHandler("/error");

            // Routing.
            app.UseRouting();

            // Authorization.
            app.UseAuthentication();
            app.UseAuthorization();

            // Prometheus.
            app.UseHttpMetrics();

            // Endpoints.
            app.UseEndpoints(endpoints =>
            {
                // Api.
                endpoints.MapControllers().RequireAuthorization();
                // Event hubs.
                endpoints.MapHub<AccountEventHub>("/api/v1/events/account").RequireAuthorization();
                endpoints.MapHub<TimeseriesEventHub>("/api/v1/events/timeseries").RequireAuthorization();
                // Health checks.
                endpoints.MapHealthChecks("/health");
                // Prometheus.
                endpoints.MapMetrics("/metrics");
            });

            // Swagger.
            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Servers = new List<OpenApiServer>()
                    {
                        new OpenApiServer
                        {
                            Url = ""
                        }
                    };
                });
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "swagger";
            });

            // ReDoc.
            app.UseReDoc(options =>
            {
                options.SpecUrl = "/swagger/v1/swagger.json";
                options.DocumentTitle = "OneGate";
                options.RoutePrefix = "redoc";
            });
        }
    }
}