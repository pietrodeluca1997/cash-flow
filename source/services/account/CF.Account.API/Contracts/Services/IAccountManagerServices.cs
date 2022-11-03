using CF.Core.Messages.IntegrationEvents;

namespace CF.Account.API.Services.Contracts
{
    public interface IAccountManagerServices
    {
        Task Create(UserCreatedEvent userCreatedEvent);
    }
}
