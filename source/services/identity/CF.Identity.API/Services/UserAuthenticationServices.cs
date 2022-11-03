using CF.Core.DTO;
using CF.Identity.API.Contracts.Services;
using CF.Identity.API.DTO.Request;
using CF.Identity.API.DTO.Response;
using CF.Identity.API.DTO.Response.Child;
using CF.Identity.API.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CF.Identity.API.Services
{
    public class UserAuthenticationServices : IUserAuthenticationServices
    {
        private readonly JwtAuthenticationSettings _jwtAuthenticationSettings;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserAuthenticationServices(IOptions<JwtAuthenticationSettings> jwtAuthenticationSettings,
                                          SignInManager<IdentityUser> signInManager,
                                          UserManager<IdentityUser> userManager)
        {
            _jwtAuthenticationSettings = jwtAuthenticationSettings.Value;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<BaseResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(loginRequestDTO.Email, loginRequestDTO.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                IdentityUser identityUser = await _userManager.FindByEmailAsync(loginRequestDTO.Email);
                IList<Claim> claims = await _userManager.GetClaimsAsync(identityUser);

                LoginResponseDTO loginResponseDTO = new()
                {
                    AccessToken = await CreateToken(identityUser, claims),
                    RefreshToken = GenerateRefreshToken(),
                    ExpiresIn = DateTime.UtcNow.AddHours(_jwtAuthenticationSettings.ExpirationTimeInHours),
                    UserInformation = new UserInformationResponseDTO
                    {
                        Id = identityUser.Id,
                        Email = identityUser.Email,
                        Claims = claims.Select(claim => new UserClaimResponseDTO { Type = claim.Type, Value = claim.Value })
                    }
                };

                return new SuccessResponseDTO<LoginResponseDTO>(HttpStatusCode.OK, "User authenticated with success", loginResponseDTO);
            }

            if (result.IsLockedOut)
            {
                return new ErrorResponseDTO(HttpStatusCode.Forbidden, "User temporarily blocked for too many invalid attempts");
            }

            return new ErrorResponseDTO(HttpStatusCode.Unauthorized, "E-mail or password invalid");
        }

        private async Task<string> CreateToken(IdentityUser identityUser, IList<Claim> claims)
        {
            ClaimsIdentity claimsIdentity = await GetClaims(identityUser, claims);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            SigningCredentials signingCredentials = GetSigningCredentials();

            SecurityTokenDescriptor securityTokenDescriptor = GenerateTokenOptions(signingCredentials, claimsIdentity);

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        private string GenerateRefreshToken()
        {
            byte[] refreshToken = new byte[32];
            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(refreshToken);

            return Convert.ToBase64String(refreshToken);
        }

        private SigningCredentials GetSigningCredentials()
        {
            byte[] jwtSecretKey = Encoding.UTF8.GetBytes(_jwtAuthenticationSettings.SecretKey);

            SymmetricSecurityKey secret = new(jwtSecretKey);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature);
        }

        private async Task<ClaimsIdentity> GetClaims(IdentityUser user, IList<Claim> claims)
        {
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (string userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            ClaimsIdentity claimsIdentity = new();
            claimsIdentity.AddClaims(claims);

            return claimsIdentity;
        }

        private SecurityTokenDescriptor GenerateTokenOptions(SigningCredentials signingCredentials, ClaimsIdentity claimsIdentity)
        {
            SecurityTokenDescriptor tokenOptions = new()
            {
                Issuer = _jwtAuthenticationSettings.Issuer,
                Subject = claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(_jwtAuthenticationSettings.ExpirationTimeInHours),
                SigningCredentials = signingCredentials,
                Audience = _jwtAuthenticationSettings.Audience,
            };

            return tokenOptions;
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }
}
