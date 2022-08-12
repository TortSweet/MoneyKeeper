using FinanceKeeper.Data.Entities;

namespace FinanceKeeper.Repositories.Abstracions
{
    public interface IFinancialCategoryRepository
    {
        public Task<FinancialCategory?> CreateCategoryAsync(FinancialCategory? category);
        public Task<FinancialCategory?> UpdateCategoryAsync(FinancialCategory? category);
        public Task<bool> DeleteCategoryAsync(int categoryId);
        public IQueryable<FinancialCategory?> GetAllCategories();
        public FinancialCategory? GetCategoryById(int categoryId);
    }
}
