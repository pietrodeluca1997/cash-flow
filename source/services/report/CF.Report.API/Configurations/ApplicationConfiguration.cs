using CF.Core.API.EventBusPublishers;
using CF.Core.Contracts.MessageBroker;
using CF.Core.Repositories;
using CF.Report.API.Data.QueryDatabase;
using CF.Report.API.EventBusConsumers;
using CF.Report.API.Settings;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CF.Report.API.Configurations
{
    public static class ApplicationConfiguration
    {

        /// <summary>
        ///     Add listener/consumers for message broker
        /// </summary>
        /// <param name="services">Extension method</param>
        /// <param name="configuration">Interface for fetching appsettings.json information</param>
        public static void AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            EventBusSettings eventBusSettings = configuration.GetSection("EventBusSettings").Get<EventBusSettings>();

            services.AddScoped<IEventBusPublisher, EventBusPublisher>();

            services.AddMassTransit(massTransitconfiguration =>
            {
                massTransitconfiguration.AddConsumer<TransactionPerformedEventConsumer>();
                massTransitconfiguration.AddConsumer<InsufficientFundsEventConsumer>();

                massTransitconfiguration.UsingRabbitMq((rabbitMqContext, rabbitMqConfiguration) =>
                {
                    rabbitMqConfiguration.Host(eventBusSettings.HostAddress);

                    rabbitMqConfiguration.ReceiveEndpoint(configuration["EventBusSettings:TransactionPerformedEventQueue"], queueConfig =>
                    {
                        queueConfig.ConfigureConsumer<TransactionPerformedEventConsumer>(rabbitMqContext);
                    });

                    rabbitMqConfiguration.ReceiveEndpoint(configuration["EventBusSettings:InsufficientFundsEventQueue"], queueConfig =>
                    {
                        queueConfig.ConfigureConsumer<InsufficientFundsEventConsumer>(rabbitMqContext);
                    });
                });

            });
        }

        public static void AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<QueryDatabaseSettings>(options => configuration.GetSection("QueryDatabaseSettings").Bind(options));
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {

        }

        /// <summary>
        ///     Inject controller/action filters into DI container
        /// </summary>
        /// <param name="services">Extension method</param>
        public static void AddActionFilters(this IServiceCollection services)
        {

        }

        /// <summary>
        ///     Inject NoSQL database into DI Container for repository pattern
        /// </summary>
        /// <param name="services">Extension method</param>
        public static void AddQueryDatabase(this IServiceCollection services)
        {
            services.AddScoped(typeof(IQueryBaseRepository<>), typeof(QueryDatabaseBaseRepository<>));
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            JwtAuthenticationSettings authSettings = configuration.GetSection("JwtAuthenticationSettings").Get<JwtAuthenticationSettings>();
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.RequireHttpsMetadata = false;
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings.SecretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = authSettings.Issuer,
                    ValidAudience = authSettings.Audience,
                    RequireExpirationTime = true
                };
            });
        }
    }
}
