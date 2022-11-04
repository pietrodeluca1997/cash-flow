using CF.Core.DTO;
using CF.Core.Messages.IntegrationEvents;

namespace CF.Account.API.Contracts.Services
{
    public interface IAccountServices
    {
        Task Credit(CreditTransactionRequestedEvent @event);
        Task Debit(DebitTransactionRequestedEvent @event);
        BaseResponseDTO GetAccount();
    }
}
