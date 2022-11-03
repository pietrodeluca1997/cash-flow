using CF.CustomMediator.Contracts;
using CF.CustomMediator.Models;
using CF.CustomMediator.Models.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace CF.CustomMediator.Core
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IRequestBehaviourContext _requestBehaviourContext;

        public Mediator(IServiceProvider serviceProvider, IRequestBehaviourContext requestBehaviourContext)
        {
            _serviceProvider = serviceProvider;
            _requestBehaviourContext = requestBehaviourContext;
        }

        public async Task SendMessage<TRequestMessage>(TRequestMessage requestMessage) where TRequestMessage : RequestMessage
        {
            IRequestMessageHandler<TRequestMessage>? requestMessageHandler = _serviceProvider.GetService(typeof(IRequestMessageHandler<TRequestMessage>)) as IRequestMessageHandler<TRequestMessage>;

            if (requestMessageHandler is not null)
            {
                await requestMessageHandler.Handle(requestMessage);
            }
            else
            {
                throw new ApplicationException($"Message: {requestMessage.GetType().Name} - doesn't have any responsible handler");
            }
        }

        public async Task Notify<TNotificationMessage>(TNotificationMessage notificationMessage) where TNotificationMessage : NotificationMessage
        {
            IEnumerable<INotificationSubscriber<TNotificationMessage>>? subscribers = _serviceProvider.GetServices(typeof(INotificationSubscriber<TNotificationMessage>)) as IEnumerable<INotificationSubscriber<TNotificationMessage>>;

            if (subscribers is not null)
            {
                foreach (INotificationSubscriber<TNotificationMessage> subscriber in subscribers) await subscriber.Receive(notificationMessage);
            }
        }

        public void FinishRequestBehaviour<TRequestMessage>(TRequestMessage requestMessage, ECommandResponseType commandResponseType) where TRequestMessage : Command
        {
            _requestBehaviourContext.Finish(requestMessage, commandResponseType);
        }

        public void FinishRequestBehaviour<TRequestMessage, TCustomCommandResponse>(TRequestMessage requestMessage, ECommandResponseType commandResponseType, TCustomCommandResponse customCommandResponse) where TRequestMessage : Command
                                                                                                                                                                                                            where TCustomCommandResponse : class
        {
            _requestBehaviourContext.Finish(requestMessage, commandResponseType, customCommandResponse);
        }
    }
}
