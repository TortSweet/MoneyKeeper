using FinanceKeeperBlazorServer.Data.Models;
using FinanceKeeperBlazorServer.Services.Interfaces;
using System.Text;
using System.Text.Json;
using FinanceKeeperBlazorServer.Data;
using Microsoft.Extensions.Options;

namespace FinanceKeeperBlazorServer.Services
{
    public class FinancialCategoryService : IBaseServices<FinancialCategory>
    {
        private readonly IFinanceKeeperApi _refitClient;
        public FinancialCategoryService(IFinanceKeeperApi client)
        {
            _refitClient = client ?? throw new ArgumentNullException(nameof(client), "Must exist");
        }

        public async Task CreateAsync(FinancialCategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Must exist");
            }
            var jsonCategory = JsonSerializer.Serialize(category);
            var content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");
            await _refitClient.CreateCategory(content);
        }

        public async Task DeleteAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException(nameof(categoryId), "Must be greater than 0");
            }
            await _refitClient.DeleteCategory(categoryId);
        }

        public async Task<IEnumerable<FinancialCategory>> GetAllAsync()
        {
            var list = await _refitClient.GetCategories();

            return list;
        }

        public async Task<FinancialCategory> GetByIdAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentNullException(nameof(categoryId), "Must be greater than 0");
            }

            var model = await _refitClient.GetCategoryById(categoryId);

            return model;
        }

        public async Task UpdateAsync(FinancialCategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Must exist");
            }

            var jsonModel = JsonSerializer.Serialize(category);
            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            await _refitClient.UpdateCategory(content);
        }
    }
}
