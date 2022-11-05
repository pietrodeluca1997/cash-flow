using CF.Account.API.Contracts.Services;
using CF.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.Account.API.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;

        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpGet]
        public BaseResponseDTO Get()
        {
            BaseResponseDTO accountResponse = _accountServices.GetAccount();

            return accountResponse;
        }
    }
}
