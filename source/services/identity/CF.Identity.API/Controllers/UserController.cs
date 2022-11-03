using CF.Core.DTO;
using CF.CustomMediator.Models;
using CF.Identity.API.Contracts.Services;
using CF.Identity.API.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace CF.Identity.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        /// <summary>
        /// Creates a new user and then process for a new account.
        /// </summary>
        /// <param name="createNewUserRequestDTO">User information</param>
        /// <returns>Result information</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/users
        ///     {
        ///         "email": "teste@gmail.com",
        ///         "password": "123456",
        ///         "passwordConfirmation": "123456",
        ///         "cpfNumber": "180.285.350-20",
        ///         "name": "John Doe"
        ///     }        
        /// </remarks>
        /// <response code="201">If process has been succeeded</response>
        /// <response code="400">If the body request or the required properties was not sent</response>
        /// <response code="409">If the e-mail is already been taken</response>
        /// <response code="422">If request information is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO<ValidationResult>), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorResponseDTO<ValidationResult>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromBody] CreateNewUserRequestDTO createNewUserRequestDTO)
        {
            BaseResponseDTO response = await _userServices.Create(createNewUserRequestDTO);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
