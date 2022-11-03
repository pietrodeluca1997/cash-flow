using CF.Core.DTO;
using CF.Identity.API.Contracts.Services;
using CF.Identity.API.DTO.Request;
using CF.Identity.API.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace CF.Identity.API.Controllers
{
    [Route("api/user-authentication")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    public class UserAuthenticationController : ControllerBase
    {
        private readonly IUserAuthenticationServices _userAuthenticationServices;

        public UserAuthenticationController(IUserAuthenticationServices userAuthenticationServices)
        {
            _userAuthenticationServices = userAuthenticationServices;
        }

        /// <summary>
        ///  Authenticates the user accordingly to access information.
        /// </summary>
        /// <param name="loginRequestDTO">Access information</param>
        /// <returns>Result information</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/users
        ///     {
        ///         "email": "teste@gmail.com",
        ///         "password": "123456"
        ///     }        
        /// </remarks>
        /// <response code="200">If process has been succeeded</response>
        /// <response code="400">If the body request or the required properties was not sent</response>
        /// <response code="401">If the access information is invalid</response>
        /// <response code="403">If the access is blocked by invalid attempts</response>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponseDTO<LoginResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            BaseResponseDTO response = await _userAuthenticationServices.Login(loginRequestDTO);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
