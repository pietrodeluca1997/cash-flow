using AutoMapper;
using CF.Identity.API.Commands.UserCommands;
using CF.Identity.API.DTO.Request;

namespace CF.Identity.API.Configuration.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateNewUserRequestDTO, CreateNewUserCommand>();
        }
    }
}
