using CF.Account.API.Contracts.RelationalDatabase;
using CF.Account.API.Contracts.Services;
using CF.Account.API.Data.MemoryDatabase;
using CF.Account.API.Data.RelationalDatabase;
using CF.Account.API.EventBusConsumers;
using CF.Account.API.Services;
using CF.Account.API.Settings;
using CF.Core.API.EventBusPublishers;
using CF.Core.Contracts.MessageBroker;
using CF.Core.Repositories;
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
        ///     Inject in memory database into DI Container for distributed cache
        /// </summary>
        /// <param name="services">Extension method</param>
        public static void AddMemoryDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["MemoryDatabaseSettings:ConnectionString"];
            });

            services.AddScoped<IMemoryDatabaseRepository, RedisBaseRepository>();
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
                massTransitconfiguration.AddConsumer<CreditTransactionRequestedConsumer>();
                massTransitconfiguration.AddConsumer<DebitTransactionRequestedConsumer>();

                massTransitconfiguration.UsingRabbitMq((rabbitMqContext, rabbitMqConfiguration) =>
                {
                    rabbitMqConfiguration.Host(eventBusSettings.HostAddress);

                    rabbitMqConfiguration.ReceiveEndpoint(configuration["EventBusSettings:UserCreatedQueue"], queueConfig =>
                    {
                        queueConfig.ConfigureConsumer<UserCreatedConsumer>(rabbitMqContext);
                    });

                    rabbitMqConfiguration.ReceiveEndpoint(configuration["EventBusSettings:CreditTransactionRequestedQueue"], queueConfig =>
                    {
                        queueConfig.ConfigureConsumer<CreditTransactionRequestedConsumer>(rabbitMqContext);
                    });

                    rabbitMqConfiguration.ReceiveEndpoint(configuration["EventBusSettings:DebitTransactionRequestedQueue"], queueConfig =>
                    {
                        queueConfig.ConfigureConsumer<DebitTransactionRequestedConsumer>(rabbitMqContext);
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
            services.AddScoped<IAccountServices, AccountServices>();
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
