using CF.Core.Contracts.MessageBroker;
using MassTransit;

namespace CF.Core.API.EventBusPublishers
{
    public class EventBusPublisher : IEventBusPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBusPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Publish<TEvent>(TEvent @event)
        {
            if (@event is not null)
            {
                await _publishEndpoint.Publish(@event);
            }
        }
    }
}
