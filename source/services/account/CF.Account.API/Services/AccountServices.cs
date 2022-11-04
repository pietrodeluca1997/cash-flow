using AutoMapper;
using CF.Account.API.Commands.AccountCommands;
using CF.Account.API.Contracts.RelationalDatabase;
using CF.Account.API.Contracts.Services;
using CF.Account.API.DTO.Response;
using CF.Core.DTO;
using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;

namespace CF.Account.API.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRepositoryManager _repositoryManager;

        public AccountServices(IMediator mediator, IMapper mapper, IRepositoryManager repositoryManager)
        {
            _mediator = mediator;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
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

        public BaseResponseDTO GetAccount()
        {
            Entities.Account defaultAccount = _repositoryManager.AccountRepository.GetById(1, trackChanges: false).First();

            return new SuccessResponseDTO<GetAccountResponseDTO>(System.Net.HttpStatusCode.OK, "Account information retrieved witch success", _mapper.Map<Entities.Account, GetAccountResponseDTO>(defaultAccount));
        }
    }
}
