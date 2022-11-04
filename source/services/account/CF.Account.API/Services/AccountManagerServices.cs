using CF.Account.API.Commands.AccountManagerCommands;
using CF.Account.API.Contracts.RelationalDatabase;
using CF.Account.API.Contracts.Services;
using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;

namespace CF.Account.API.Services
{
    public class AccountManagerServices : IAccountManagerServices
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMediator _mediator;

        public AccountManagerServices(IRepositoryManager repositoryManager, IMediator mediator)
        {
            _repositoryManager = repositoryManager;
            _mediator = mediator;
        }

        public async Task Create(UserCreatedEvent userCreatedEvent)
        {
            try
            {
                //Get the default account by sake of simplicity
                Entities.Account account = _repositoryManager.AccountRepository.GetById(1, trackChanges: false).First();

                CreateNewAccountManagerCommand command = new()
                {
                    AccountIdentifier = account.UniqueIdentifier,
                    CPFNumber = userCreatedEvent.CPFNumber.Replace(".", string.Empty).Replace("-", string.Empty).Trim(),
                    Name = userCreatedEvent.Name,
                    UserId = Guid.Parse(userCreatedEvent.UserId)
                };

                await _mediator.SendMessage(command);
            }
            catch
            {
                AccountManagerFailedCreationEvent @event = new(Guid.Parse(userCreatedEvent.UserId));
                await _mediator.Notify(@event);
            }
        }
    }
}
