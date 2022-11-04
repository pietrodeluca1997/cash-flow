using AutoMapper;
using CF.Account.API.Commands.AccountCommands;
using CF.Account.API.Contracts.Services;
using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;

namespace CF.Account.API.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountServices(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Credit(CreditTransactionRequestedEvent @event)
        {
            CreditAccountCommand creditAccountCommand = _mapper.Map<CreditTransactionRequestedEvent, CreditAccountCommand>(@event);

            await _mediator.SendMessage(creditAccountCommand);
        }

        public async Task Debit(DebitTransactionRequestedEvent @event)
        {
            DebitAccountCommand debitAccountCommand = _mapper.Map<DebitTransactionRequestedEvent, DebitAccountCommand>(@event);

            await _mediator.SendMessage(debitAccountCommand);
        }
    }
}
