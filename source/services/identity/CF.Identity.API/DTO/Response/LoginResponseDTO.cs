using CF.Identity.API.DTO.Response.Child;

namespace CF.Identity.API.DTO.Response
{
    public class LoginResponseDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public UserInformationResponseDTO UserInformation { get; set; }

        public LoginResponseDTO()
        {

        }

        public LoginResponseDTO(string accessToken, string refreshToken, DateTime expiresIn, UserInformationResponseDTO userInformation)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            ExpiresIn = expiresIn;
            UserInformation = userInformation;
        }
    }
}
