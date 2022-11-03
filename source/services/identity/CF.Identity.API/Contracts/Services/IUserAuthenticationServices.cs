using CF.Core.DTO;
using CF.Identity.API.DTO.Request;

namespace CF.Identity.API.Contracts.Services
{
    public interface IUserAuthenticationServices
    {
        Task<BaseResponseDTO> Login(LoginRequestDTO loginRequestDTO);
    }
}
