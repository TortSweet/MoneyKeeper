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
    public class FinancialCategoryController : Controller
    {
        private readonly IBaseServices<FinancialCategoryDto> _services;
        private readonly ILogger<FinancialCategoryController> _logger;

        public FinancialCategoryController(IBaseServices<FinancialCategoryDto> services, ILogger<FinancialCategoryController> logger)
        {
            _services = services ??
                        throw new ArgumentNullException(nameof(services), "Financial service must be exist");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Financial logger must be exist");
        }
        /// <summary>
        /// Create new entry about category into DB
        /// </summary>
        /// <param name="category">Entry of Category</param>
        /// <returns>Return response than entry were write</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialCategoryDto>> CreateCategoryAsync(FinancialCategoryDto category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            _logger.LogInformation($"Create Category: {DateTime.Now}");
            var newCategory = await _services.CreateEntryAsync(category);
            return Ok(newCategory);
        }
        /// <summary>
        /// Updates an existing entry of category in the DB
        /// </summary>
        /// <param name="category">Entry of Category</param>
        /// <returns>Updated entry of category</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FinancialCategoryDto>> UpdateCategoryAsync(FinancialCategoryDto category)
        {
            if (category == null)
            {
                return BadRequest();
            }
            _logger.LogInformation($"Update Category: {DateTime.Now}");
            var updatedCategory = await _services.UpdateEntryAsync(category);
            return Ok(updatedCategory);
        }
        /// <summary>
        /// Delete entry of categories from DB
        /// </summary>
        /// <param name="categoryId">Id of category</param>
        /// <returns>Boolean result from removing</returns>
        [HttpDelete("{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest();
            }
            _logger.LogInformation($"Delete Category: {DateTime.Now}");
            return Ok(await _services.DeleteEntryAsync(categoryId));
        }
        /// <summary>
        /// Show all an existing entries of categories into DB
        /// </summary>
        /// <returns>List of categories</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAllCategories()
        {
            _logger.LogInformation($"Get all Category: {DateTime.Now}");
            var allCategories =  _services.GetAllEntries();

            return Ok(allCategories);
        }
        /// <summary>
        /// Show existing entries of category by categoryId into DB
        /// </summary>
        /// <param name="categoryId">Id of category</param>
        /// <returns>Category that matched the id</returns>
        [HttpGet("{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<FinancialCategoryDto> GetCategoryById(int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest();
            }
            _logger.LogInformation($"Get Categories by id: {DateTime.Now}");
            var category = _services.GetEntryById(categoryId);

            return Ok(category);
        }
    }
}
