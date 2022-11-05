using CF.Core.DTO;
using CF.Core.Repositories;
using CF.Report.API.Data.QueryDatabase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CF.Report.API.Controllers
{
    [Route("api/reports")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Produces("application/json")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IQueryBaseRepository<TransactionReportDocument> _transactionReportRepository;

        public ReportController(IQueryBaseRepository<TransactionReportDocument> transactionReportRepository)
        {
            _transactionReportRepository = transactionReportRepository;
        }


        /// <summary>
        ///    Get daily transactions report
        /// </summary>
        /// <param name="day">Day desired</param>
        /// <param name="month">Month desired</param>
        /// <param name="year">Year desired</param>
        /// <returns>Result information</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/reports/daily-transactions-report?day=4&month=11&year=2022
        ///     
        /// </remarks>  
        /// <response code="200">If process has been accepted for further processment</response>
        [ProducesResponseType(typeof(SuccessResponseDTO<IList<TransactionReportDocument>>), StatusCodes.Status200OK)]
        [HttpGet("daily-transactions-report")]
        public async Task<IActionResult> GetDailyTransactionsReport(int day, int month, int year)
        {
            DateTime initialDate = new DateTime(year, month, day);
            DateTime endDate = new DateTime(year, month, day).AddDays(1);

            IList<TransactionReportDocument> query = await _transactionReportRepository.FilterByAsync
                                                            (filter => filter.TransactionDate > initialDate &&
                                                            filter.TransactionDate < endDate);

            SuccessResponseDTO<IList<TransactionReportDocument>> responseDTO = new(HttpStatusCode.OK, "Transactions retrieved with success.", query);

            return Ok(responseDTO);
        }
    }
}
