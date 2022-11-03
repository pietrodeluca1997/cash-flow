using AutoMapper;
using CF.Core.DTO;
using CF.Core.Messages.IntegrationEvents;
using CF.CustomMediator.Contracts;
using CF.Identity.API.Commands.UserCommands;
using CF.Identity.API.Contracts.Services;
using CF.Identity.API.DTO.Request;

namespace CF.Identity.API.Services
{
    public class UserServices : IUserServices
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRequestBehaviourContext _requestBehaviourContext;

        public UserServices(IMediator mediator, IMapper mapper, IRequestBehaviourContext requestBehaviourContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _requestBehaviourContext = requestBehaviourContext;
        }

        public async Task<BaseResponseDTO> Create(CreateNewUserRequestDTO createNewUserRequestDTO)
        {
            CreateNewUserCommand command = _mapper.Map<CreateNewUserRequestDTO, CreateNewUserCommand>(createNewUserRequestDTO);

            await _mediator.SendMessage(command);

            return ResponseDTOFactory.CreateFromRequestBehaviour(_requestBehaviourContext);
        }

        public async Task Delete(AccountManagerFailedCreationEvent accountManagerFailedCreationEvent)
        {
            DeleteUserCommand command = new(accountManagerFailedCreationEvent.UserId);

            await _mediator.SendMessage(command);
        }
    }
}
