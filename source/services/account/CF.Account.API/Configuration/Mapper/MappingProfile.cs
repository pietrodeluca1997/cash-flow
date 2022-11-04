using AutoMapper;
using CF.Account.API.Commands.AccountCommands;
using CF.Account.API.DTO.Response;
using CF.Core.Messages.IntegrationEvents;

namespace CF.Account.API.Configuration.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreditTransactionRequestedEvent, CreditAccountCommand>();
            CreateMap<DebitTransactionRequestedEvent, DebitAccountCommand>();
            CreateMap<Entities.Account, GetAccountResponseDTO>();
        }
    }
}
