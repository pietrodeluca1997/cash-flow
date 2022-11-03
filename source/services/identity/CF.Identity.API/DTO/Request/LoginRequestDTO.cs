using CF.Core.DTO;

namespace CF.Identity.API.DTO.Request
{
    public class LoginRequestDTO : BaseRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginRequestDTO()
        {

        }

        public LoginRequestDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
