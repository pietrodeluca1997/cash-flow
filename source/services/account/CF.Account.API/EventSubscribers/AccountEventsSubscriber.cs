using CF.Core.Contracts.MessageBroker;
using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;

namespace CF.Account.API.EventSubscribers
{
    public class AccountEventsSubscriber : INotificationSubscriber<TransactionPerformedEvent>,
                                           INotificationSubscriber<AccountInsufficientFundsEvent>
    {
        private readonly IEventBusPublisher _eventBusPublisher;

        public AccountEventsSubscriber(IEventBusPublisher eventBusPublisher)
        {
            _eventBusPublisher = eventBusPublisher;
        }

        public async Task Receive(AccountInsufficientFundsEvent notificationMessage)
        {
            await _eventBusPublisher.Publish(notificationMessage);
        }

        public async Task Receive(TransactionPerformedEvent notificationMessage)
        {
            await _eventBusPublisher.Publish(notificationMessage);
        }
    }
}
