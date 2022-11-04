using CF.Core.Messages.IntegrationEvents;

namespace CF.Account.API.Contracts.Services
{
    public interface IAccountManagerServices
    {
        Task Create(UserCreatedEvent userCreatedEvent);
    }
}
