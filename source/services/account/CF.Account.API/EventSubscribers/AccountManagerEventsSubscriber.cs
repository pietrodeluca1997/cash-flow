using CF.Core.Contracts.MessageBroker;
using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;

namespace CF.Account.API.EventSubscribers
{
    public class AccountManagerEventsSubscriber : INotificationSubscriber<AccountManagerFailedCreationEvent>
    {
        private readonly IEventBusPublisher _eventBusPublisher;

        public AccountManagerEventsSubscriber(IEventBusPublisher eventBusPublisher)
        {
            _eventBusPublisher = eventBusPublisher;
        }

        public async Task Receive(AccountManagerFailedCreationEvent notificationMessage)
        {
            await _eventBusPublisher.Publish(notificationMessage);
        }
    }
}
