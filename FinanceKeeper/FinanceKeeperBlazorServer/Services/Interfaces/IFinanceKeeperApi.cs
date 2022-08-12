using FinanceKeeperBlazorServer.Data.Models;
using Refit;

namespace FinanceKeeperBlazorServer.Services.Interfaces
{
    public interface IFinanceKeeperApi
    {
        [Get("/FinancialCategory")]
        Task<IEnumerable<FinancialCategory>> GetCategories();
        [Get("/FinancialCategory/{categoryId}")]
        Task<FinancialCategory> GetCategoryById([Body] int categoryId);
        [Post("/FinancialCategory")]
        Task<FinancialCategory> CreateCategory([Body] HttpContent content);
        [Delete("/FinancialCategory/{categoryId}")]
        Task DeleteCategory([Body] int categoryId);
        [Put("/FinancialCategory")]
        Task<FinancialCategory> UpdateCategory([Body] HttpContent content);

        [Get("/FinancialOperation")]
        Task<IEnumerable<FinancialOperation>> GetAllOperations();
        [Get("/FinancialOperation/{operationId}")]
        Task<FinancialOperation> GetOperationById([Body] int operationId);
        [Post("/FinancialOperation")]
        Task<FinancialOperation> CreateOperation([Body] HttpContent content);
        [Delete("/FinancialOperation/{operationId}")]
        Task DeleteOperation([Body] int operationId);
        [Put("/FinancialOperation")]
        Task<FinancialOperation> UpdateOperation([Body] HttpContent content);

        [Get("/MoneyReport/{date}")]
        Task<MoneyReport> ReportPerDay([Query] string date);
        [Get("/MoneyReport/{startDay}&{endDay}")]
        Task<MoneyReport> ReportPerPeriod([Query] string startDay, string endDay);
    }
}
