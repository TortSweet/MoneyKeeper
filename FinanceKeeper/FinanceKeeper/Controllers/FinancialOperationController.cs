using System.Net.Mime;
using FinanceKeeper.Dtos;
using FinanceKeeper.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FinanceKeeper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class FinancialOperationController : Controller
    {
        private readonly IBaseServices<FinancialOperationDto?> _services;
        private readonly ILogger<FinancialOperationController> _logger;
        public FinancialOperationController(IBaseServices<FinancialOperationDto?> services, ILogger<FinancialOperationController> logger)
        {
            _services = services ??
                        throw new ArgumentNullException(nameof(services), "Financial service must be exist");
            _logger = logger ??
                      throw new ArgumentNullException(nameof(logger), "logger must be exist");
        }

        /// <summary>
        /// Create new entry about operation into DB
        /// </summary>
        /// <param name="operation">Entry of operation</param>
        /// <returns>Return response than entry were write</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialOperationDto>> CreateOperationAsync(FinancialOperationDto? operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }
            _logger.LogInformation($"Create Operation: {DateTime.Now}");
            var newOperation = await _services.CreateEntryAsync(operation);
            return Ok(newOperation);
        }
        /// <summary>
        /// Updates an existing entry of operation in the DB
        /// </summary>
        /// <param name="operation">Entry of operation</param>
        /// <returns>Updated entry of operation</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialOperationDto>> UpdateOperationAsync(FinancialOperationDto? operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }
            _logger.LogInformation($"Update Operation: {DateTime.Now}");
            var updatedOperation = await _services.UpdateEntryAsync(operation);
            return Ok(updatedOperation);
        }
        /// <summary>
        /// Delete entry of operation from DB
        /// </summary>
        /// <param name="operationId">Id of operation</param>
        /// <returns>Boolean result from removing</returns>
        [HttpDelete("{operationId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteOperationAsync(int operationId)
        {
            if (operationId <= 0)
            {
                return BadRequest();
            }
            _logger.LogInformation($"Delete Operation: {DateTime.Now}");
            return Ok(await _services.DeleteEntryAsync(operationId));
        }
        /// <summary>
        /// Show all an existing entries of operations into DB
        /// </summary>
        /// <returns>List of operations</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAllOperations()
        {
            var allOperatios = _services.GetAllEntries();
            _logger.LogInformation($"Get all Operation: {DateTime.Now}");

            return Ok(allOperatios);
        }
        /// <summary>
        /// Show existing entries of operation by categoryId into DB
        /// </summary>
        /// <param name="operationId">Id of operation</param>
        /// <returns>Operation that matched the id</returns>
        [HttpGet("{operationId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FinancialOperationDto> GetOperationById(int operationId)
        {
            if (operationId <= 0)
            {
                return BadRequest();
            }
            _logger.LogInformation($"Get operation by id Operation: {DateTime.Now}");

            var operation = _services.GetEntryById(operationId);

            return Ok(operation);
        }
    }
}
