using CF.CustomMediator.Contracts;
using CF.CustomMediator.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;

namespace CF.CustomMediator.Builders
{
    public class MediatorBuilder : IDisposable
    {
        private readonly Type[] _projectRootAssemblyTypes;
        private readonly ConcurrentDictionary<Type, Type> InterfaceToImplementationMapper;

        public MediatorBuilder(Type classTypeFromProjectRoot)
        {
            _projectRootAssemblyTypes = classTypeFromProjectRoot.Assembly.GetTypes();
            InterfaceToImplementationMapper = new ConcurrentDictionary<Type, Type>();
        }

        public static MediatorBuilder CreateBuilder(Type classTypeFromProjectRoot)
        {
            return new(classTypeFromProjectRoot);
        }

        public MediatorBuilder AddHandlers()
        {
            if (_projectRootAssemblyTypes is not null)
            {
                foreach (Type type in _projectRootAssemblyTypes)
                {
                    IEnumerable<Type>? interfaces = type.GetInterfaces().Where(type =>
                                                        type.IsGenericType &&
                                                        (type.GetGenericTypeDefinition() == typeof(IRequestMessageHandler<>) ||
                                                        type.GetGenericTypeDefinition() == typeof(INotificationSubscriber<>)));

                    if (interfaces is not null)
                    {
                        foreach (Type interfaceType in interfaces)
                        {
                            InterfaceToImplementationMapper.TryAdd(interfaceType, type);
                        }
                    }
                }
            }

            return this;
        }

        public void Build(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IMediator, Mediator>();
            serviceCollection.AddScoped<IRequestBehaviourContext, RequestBehaviourContext>();

            foreach (KeyValuePair<Type, Type> keyValuePair in InterfaceToImplementationMapper)
            {
                serviceCollection.AddScoped(keyValuePair.Key, keyValuePair.Value);
            }

            Dispose();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
