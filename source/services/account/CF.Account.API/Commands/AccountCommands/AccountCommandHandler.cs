using CF.Account.API.Contracts.RelationalDatabase;
using CF.Core.DomainObjects.Enums;
using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;

namespace CF.Account.API.Commands.AccountCommands
{
    public class AccountCommandHandler : IRequestMessageHandler<CreditAccountCommand>,
                                         IRequestMessageHandler<DebitAccountCommand>
    {
        private readonly IMediator _mediator;
        private readonly IRepositoryManager _repositoryManager;

        public AccountCommandHandler(IMediator mediator, IRepositoryManager repositoryManager)
        {
            _mediator = mediator;
            _repositoryManager = repositoryManager;
        }

        public async Task Handle(DebitAccountCommand requestMessage)
        {
            if (requestMessage.IsValid())
            {
                Entities.Account defaultAccount = _repositoryManager.AccountRepository.GetById(1, trackChanges: false).First();

                if (!defaultAccount.Debit(requestMessage.MoneyAmount))
                {
                    //Insufficient funds
                    AccountInsufficientFundsEvent accountInsufficientFundsEvent = new();
                    await _mediator.Notify(accountInsufficientFundsEvent);
                    return;
                }
                else
                {
                    _repositoryManager.AccountRepository.Update(defaultAccount);

                    _repositoryManager.Save();

                    TransactionPerformedEvent transactionPerformedEvent = new(ETransactionType.Debit)
                    {
                        TransactionAmount = requestMessage.MoneyAmount,
                        UserId = requestMessage.UserId,
                        Email = requestMessage.Email,
                        TransactionDescription = requestMessage.Description
                    };

                    await _mediator.Notify(transactionPerformedEvent);
                }
            }
        }

        public async Task Handle(CreditAccountCommand requestMessage)
        {
            if (requestMessage.IsValid())
            {
                Entities.Account defaultAccount = _repositoryManager.AccountRepository.GetById(1, trackChanges: false).First();

                defaultAccount.Credit(requestMessage.MoneyAmount);

                _repositoryManager.AccountRepository.Update(defaultAccount);

                _repositoryManager.Save();

                TransactionPerformedEvent transactionPerformedEvent = new(ETransactionType.Credit)
                {
                    TransactionAmount = requestMessage.MoneyAmount,
                    UserId = requestMessage.UserId,
                    Email = requestMessage.Email,
                    TransactionDescription = requestMessage.Description
                };

                await _mediator.Notify(transactionPerformedEvent);
            }
        }
    }
}
