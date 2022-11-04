using CF.Core.API.EventBusPublishers;
using CF.Core.Contracts.MessageBroker;
using CF.Report.API.Settings;
using MassTransit;

namespace CF.Report.API.Configurations
{
    public static class ApplicationConfiguration
    {
        public static void AddQueryDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IRepositoryManager, RepositoryManager>();
            QueryDatabaseSettings relationalDatabaseSettings = configuration.GetSection("QueryDatabaseSettings")
                                                                            .Get<QueryDatabaseSettings>();

            //services.AddDbContext<ReportDbContext>(options => options.UseNpgsql(relationalDatabaseSettings.ConnectionString));
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
                massTransitconfiguration.UsingRabbitMq((rabbitMqContext, rabbitMqConfiguration) =>
                {
                    rabbitMqConfiguration.Host(eventBusSettings.HostAddress);
                });
            });
        }

        public static void AddApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<QueryDatabaseSettings>(options => configuration.GetSection("RelationalDatabaseSettings").Bind(options));
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
    }
}
