using FinanceKeeper.Data.Entities;

namespace FinanceKeeper.Repositories.Abstracions
{
    public interface IFinancialOperationRepository
    {
        public Task<FinancialOperation?> CreateOperationAsync(FinancialOperation? category);
        public Task<FinancialOperation?> UpdateOperationAsync(FinancialOperation? category);
        public Task<bool> DeleteOperationAsync(int categoryId);
        public IQueryable<FinancialOperation?> GetAllOperations();
        public FinancialOperation? GetOperationById (int categoryId);
    }
}
