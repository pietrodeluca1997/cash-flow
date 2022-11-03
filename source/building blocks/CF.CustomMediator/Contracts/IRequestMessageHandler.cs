using CF.CustomMediator.Models;

namespace CF.CustomMediator.Contracts
{
    public interface IRequestMessageHandler<TRequestMessage> where TRequestMessage : RequestMessage
    {
        Task Handle(TRequestMessage requestMessage);
    }
}
