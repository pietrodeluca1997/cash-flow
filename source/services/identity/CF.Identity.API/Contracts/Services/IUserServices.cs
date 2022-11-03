using CF.Core.DTO;
using CF.Core.Messages.IntegrationEvents;
using CF.Identity.API.DTO.Request;

namespace CF.Identity.API.Contracts.Services
{
    public interface IUserServices
    {
        Task<BaseResponseDTO> Create(CreateNewUserRequestDTO createNewUserRequestDTO);
        Task Delete(AccountManagerFailedCreationEvent accountManagerFailedCreationEvent);
    }
}
