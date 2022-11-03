using CF.Core.API.EventBusPublishers;
using CF.Core.Contracts.MessageBroker;
using CF.Identity.API.Contracts.Services;
using CF.Identity.API.Data.RelationalDatabase;
using CF.Identity.API.EventBusConsumers;
using CF.Identity.API.Services;
using CF.Identity.API.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CF.Identity.API.Configuration
{
    public static class ApplicationConfiguration
    {
        public static void AddRelationalDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            RelationalDatabaseSettings relationalDatabaseSettings = configuration.GetSection("RelationalDatabaseSettings")
                                                                                 .Get<RelationalDatabaseSettings>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(relationalDatabaseSettings.ConnectionString));
        }

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
                massTransitconfiguration.AddConsumer<AccountManagerFailedCreationConsumer>();

                massTransitconfiguration.UsingRabbitMq((rabbitMqContext, rabbitMqConfiguration) =>
                {
                    rabbitMqConfiguration.Host(eventBusSettings.HostAddress);

                    rabbitMqConfiguration.ReceiveEndpoint(configuration["EventBusSettings:AccountManagerFailedCreationQueue"], queueConfig =>
                    {
                        queueConfig.ConfigureConsumer<AccountManagerFailedCreationConsumer>(rabbitMqContext);
                    });
                });
            });
        }

        public static void AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RelationalDatabaseSettings>(options => configuration.GetSection("RelationalDatabaseSettings").Bind(options));
            services.Configure<JwtAuthenticationSettings>(options => configuration.GetSection("JwtAuthenticationSettings").Bind(options));
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IUserAuthenticationServices, UserAuthenticationServices>();
        }

        /// <summary>
        ///     Inject controller/action filters into DI container
        /// </summary>
        /// <param name="services">Extension method</param>
        public static void AddActionFilters(this IServiceCollection services)
        {

        }
    }
}
