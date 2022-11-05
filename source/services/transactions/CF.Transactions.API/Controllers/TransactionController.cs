using CF.Core.DTO;
using CF.Transactions.API.Contracts.Services;
using CF.Transactions.API.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CF.Transactions.API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;

        public TransactionController(ITransactionServices transactionServices)
        {
            _transactionServices = transactionServices;
        }

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
        ///         "moneyAmount": 100.0,
        ///         "description": "Transaction"
        ///     }        
        /// </remarks>  
        /// <response code="202">If process has been accepted for further processment</response>
        /// <response code="400">If the body request or the required properties was not sent</response>
        /// <response code="422">If request information is invalid</response>
        [HttpPost("debit")]
        [ProducesResponseType(typeof(SuccessResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Debit(CreateNewTransactionRequestDTO createNewTransactionRequestDTO)
        {
            BaseResponseDTO responseDTO = await _transactionServices.Debit(createNewTransactionRequestDTO);

            return StatusCode((int)responseDTO.StatusCode, responseDTO);

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
        ///         "moneyAmount": 100.0,
        ///         "description": "Transaction"
        ///     }        
        /// </remarks>  
        /// <response code="202">If process has been accepted for further processment</response>
        /// <response code="400">If the body request or the required properties was not sent</response>
        /// <response code="422">If request information is invalid</response>
        [HttpPost("credit")]
        [ProducesResponseType(typeof(SuccessResponseDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Credit(CreateNewTransactionRequestDTO createNewTransactionRequestDTO)
        {
            BaseResponseDTO responseDTO = await _transactionServices.Credit(createNewTransactionRequestDTO);

            return StatusCode((int)responseDTO.StatusCode, responseDTO);
        }
    }
}
