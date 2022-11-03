using CF.CustomMediator.Builders;
using Microsoft.Extensions.DependencyInjection;

namespace CF.CustomMediator.Configuration
{
    public static class MediatorConfiguration
    {
        public static void AddMediator(this IServiceCollection serviceCollection, Type classTypeFromProjectRoot)
        {
            MediatorBuilder.CreateBuilder(classTypeFromProjectRoot)
                           .AddHandlers()
                           .Build(serviceCollection);
        }
    }
}
