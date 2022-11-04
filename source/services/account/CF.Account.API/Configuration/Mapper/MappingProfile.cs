using AutoMapper;
using CF.Account.API.Commands.AccountCommands;
using CF.Core.Messages.IntegrationEvents;

namespace CF.Account.API.Configuration.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreditTransactionRequestedEvent, CreditAccountCommand>();
            CreateMap<DebitTransactionRequestedEvent, DebitAccountCommand>();
        }
    }
}
