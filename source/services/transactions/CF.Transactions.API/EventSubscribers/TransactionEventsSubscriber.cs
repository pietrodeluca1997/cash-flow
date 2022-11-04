using CF.Core.Contracts.MessageBroker;
using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;

namespace CF.Transactions.API.EventSubscribers
{
    public class TransactionEventsSubscriber : INotificationSubscriber<CreditTransactionRequestedEvent>,
                                               INotificationSubscriber<DebitTransactionRequestedEvent>
    {
        private readonly IEventBusPublisher _eventBusPublisher;

        public TransactionEventsSubscriber(IEventBusPublisher eventBusPublisher)
        {
            _eventBusPublisher = eventBusPublisher;
        }

        public async Task Receive(CreditTransactionRequestedEvent notificationMessage)
        {
            await _eventBusPublisher.Publish(notificationMessage);
        }

        public async Task Receive(DebitTransactionRequestedEvent notificationMessage)
        {
            await _eventBusPublisher.Publish(notificationMessage);
        }
    }
}
