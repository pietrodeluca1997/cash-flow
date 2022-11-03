namespace CF.Identity.API.DTO.Response.Child
{
    public class UserClaimResponseDTO
    {
        public string Value { get; set; }
        public string Type { get; set; }

        public UserClaimResponseDTO()
        {

        }

        public UserClaimResponseDTO(string value, string type)
        {
            Value = value;
            Type = type;
        }
    }
}
