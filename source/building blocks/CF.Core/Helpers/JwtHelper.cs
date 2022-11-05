using CF.Core.DomainObjects;
using System.IdentityModel.Tokens.Jwt;

namespace CF.Core.Helpers
{
    public static class JwtHelper
    {
        private static JwtSecurityToken Read(string tokenString)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            return jwtSecurityTokenHandler.ReadJwtToken(tokenString);
        }

        public static User RetrieveUserData(string userToken)
        {
            JwtSecurityToken jwtSecurityToken = Read(userToken);

            User user = new();

            user.UserId = Guid.Parse(jwtSecurityToken.Claims.Where(claim => claim.Type.Equals(JwtRegisteredClaimNames.Sub)).First().Value);
            user.Email = jwtSecurityToken.Claims.Where(claim => claim.Type.Equals(JwtRegisteredClaimNames.Email)).First().Value;

            return user;
        }
    }
}
