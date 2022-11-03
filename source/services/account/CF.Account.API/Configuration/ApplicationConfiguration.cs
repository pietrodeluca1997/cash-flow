using CF.Account.API.Contracts.RelationalDatabase;
using CF.Account.API.Data.RelationalDatabase;
using CF.Account.API.EventBusConsumers;
using CF.Account.API.Services;
using CF.Account.API.Services.Contracts;
using CF.Account.API.Settings;
using CF.Core.API.EventBusPublishers;
using CF.Core.Contracts.MessageBroker;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CF.Account.API.Configuration
{
    public static class ApplicationConfiguration
    {
        public static void AddRelationalDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            RelationalDatabaseSettings relationalDatabaseSettings = configuration.GetSection("RelationalDatabaseSettings")
                                                                                 .Get<RelationalDatabaseSettings>();

            services.AddDbContext<AccountDbContext>(options => options.UseNpgsql(relationalDatabaseSettings.ConnectionString));
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
                massTransitconfiguration.AddConsumer<UserCreatedConsumer>();

                massTransitconfiguration.UsingRabbitMq((rabbitMqContext, rabbitMqConfiguration) =>
                {
                    rabbitMqConfiguration.Host(eventBusSettings.HostAddress);

                    rabbitMqConfiguration.ReceiveEndpoint(configuration["EventBusSettings:UserCreatedQueue"], queueConfig =>
                    {
                        queueConfig.ConfigureConsumer<UserCreatedConsumer>(rabbitMqContext);
                    });
                });
            });
        }

        public static void AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RelationalDatabaseSettings>(options => configuration.GetSection("RelationalDatabaseSettings").Bind(options));
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountManagerServices, AccountManagerServices>();
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
