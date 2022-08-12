using System.Net.Mime;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;


namespace FinanceKeeper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class MoneyReportController : Controller
    {
        private readonly IMoneyReport _service;
        private readonly ILogger<MoneyReportController> _logger;
        public MoneyReportController(IMoneyReport service, ILogger<MoneyReportController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service), "Must exist");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Must exist");
        }
        /// <summary>
        /// Get report of financial operations on a given day
        /// </summary>
        /// <param name="date">A day to do report</param>
        /// <returns>Money Report by day</returns>
        [HttpGet("{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IMoneyReport> GetDayReport(string? date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return BadRequest();
            }
            _logger.LogInformation($"Get report per day: {DateTime.Now}");

            var newReport  = _service.GetMoneyReportByDay(date);
            return Ok(newReport);
        }
        /// <summary>
        /// Get report of financial operations by period
        /// </summary>
        /// <param name="startDay">Start period day</param>
        /// <param name="endDay">Close period day</param>
        /// <returns>Money report by period</returns>
        [HttpGet("{startDay}&{endDay}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IMoneyReport> GetPeriodReport(string? startDay, string? endDay)
        {
            if (string.IsNullOrEmpty(startDay))
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(endDay))
            {
                return BadRequest();
            }
            _logger.LogInformation($"Get report by period: {DateTime.Now}");

            var newReport = _service.GetMoneyReportForThePeriod(startDay, endDay);
            return Ok(newReport);
        }
    }
}
