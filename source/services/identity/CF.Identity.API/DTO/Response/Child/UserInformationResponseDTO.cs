namespace CF.Identity.API.DTO.Response.Child
{
    public class UserInformationResponseDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaimResponseDTO> Claims { get; set; }

        public UserInformationResponseDTO()
        {

        }

        public UserInformationResponseDTO(string id, string email, IEnumerable<UserClaimResponseDTO> claims)
        {
            Id = id;
            Email = email;
            Claims = claims;
        }
    }
}
