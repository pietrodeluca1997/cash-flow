namespace CF.Core.Contracts.MessageBroker
{
    public interface IEventBusPublisher
    {
        Task Publish<TEvent>(TEvent @event);
    }
}
