using CF.Core.Contracts.MessageBroker;
using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;

namespace CF.Identity.API.EventSubscribers
{
    public class UserEventsSubscriber : INotificationSubscriber<UserCreatedEvent>
    {
        private readonly IEventBusPublisher _eventBusPublisher;

        public UserEventsSubscriber(IEventBusPublisher eventBusPublisher)
        {
            _eventBusPublisher = eventBusPublisher;
        }

        public async Task Receive(UserCreatedEvent notificationMessage)
        {
            await _eventBusPublisher.Publish(notificationMessage);
        }
    }
}
