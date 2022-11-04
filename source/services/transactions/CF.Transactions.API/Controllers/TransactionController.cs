using CF.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CF.Transactions.API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    public class TransactionController : ControllerBase
    {
        /// <summary>
        /// Receives a new debit transaction for further processment.
        /// </summary>
        /// <param name="createNewTransactionRequestDTO">Debit transaction information</param>
        /// <returns>Result information</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/transactions/debit
        ///     {   
        ///         
        ///     }        
        /// </remarks>  
        /// <response code="202">If process has been accepted for further processment</response>
        /// <response code="400">If the body request or the required properties was not sent</response>
        /// <response code="422">If request information is invalid</response>
        [HttpPost("debit")]
        [ProducesResponseType(typeof(SuccessResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Debit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Receives a new credit transaction for further processment.
        /// </summary>
        /// <param name="createNewTransactionRequestDTO">Debit transaction information</param>
        /// <returns>Result information</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/transactions/credit
        ///     {   
        ///         
        ///     }        
        /// </remarks>  
        /// <response code="202">If process has been accepted for further processment</response>
        /// <response code="400">If the body request or the required properties was not sent</response>
        /// <response code="422">If request information is invalid</response>
        [HttpPost("credit")]
        [ProducesResponseType(typeof(SuccessResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Credit()
        {
            throw new NotImplementedException();
        }
    }
}
