using CF.Account.API.Contracts.RelationalDatabase;
using CF.Account.API.Entities;
using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;

namespace CF.Account.API.Commands.AccountManagerCommands
{
    public class AccountManagerCommandHandler : IRequestMessageHandler<CreateNewAccountManagerCommand>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryManager _repositoryManager;

        public AccountManagerCommandHandler(IMediator mediator, IRepositoryManager repositoryManager)
        {
            _mediator = mediator;
            _repositoryManager = repositoryManager;
        }

        public async Task Handle(CreateNewAccountManagerCommand requestMessage)
        {
            AccountManagerFailedCreationEvent @event = new(requestMessage.UserId);

            if (!requestMessage.IsValid())
            {
                await _mediator.Notify(@event);
                return;
            }


            bool isCpfAlreadyInUse = _repositoryManager.AccountManagerRepository.GetByCPFNumber(requestMessage.CPFNumber, trackChanges: false).FirstOrDefault() is not null;

            if (isCpfAlreadyInUse)
            {
                //Error event
                await _mediator.Notify(@event);
                return;
            }

            AccountManager accountManagerForCreation = new()
            {
                Name = requestMessage.Name,
                CPFNumber = requestMessage.CPFNumber.Replace(".", string.Empty).Replace("-", string.Empty).Trim(),
                UniqueIdentifier = requestMessage.UserId,
                AccountIdentifier = requestMessage.AccountIdentifier
            };

            _repositoryManager.AccountManagerRepository.Create(accountManagerForCreation);

            _repositoryManager.Save();
        }
    }
}