using CF.Core.DTO;

namespace CF.Identity.API.DTO.Request
{
    public class CreateNewUserRequestDTO : BaseRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public string CPFNumber { get; set; }
        public string Name { get; set; }

        public CreateNewUserRequestDTO()
        {

        }

        public CreateNewUserRequestDTO(string email, string password, string passwordConfirmation, string cPFNumber, string name)
        {
            Email = email;
            Password = password;
            PasswordConfirmation = passwordConfirmation;
            CPFNumber = cPFNumber;
            Name = name;
        }
    }
}
