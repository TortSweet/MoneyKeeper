using FinanceKeeper.Data;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Repositories.Abstracions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FinanceKeeper.Repositories
{
    public class FinancialOperationRepository : IFinancialOperationRepository
    {
        private readonly AppDbContext _context;

        public FinancialOperationRepository(AppDbContext? context)
        {
            _context = context ?? throw new ArgumentNullException("AppDbContext can't be null", nameof(context));
        }

        public async Task<FinancialOperation?> CreateOperationAsync(FinancialOperation? operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation), "cant be null");
            }

            var newOperation = _context.FinancialOperations?.Add(operation);
            if (newOperation == null)
            {
                throw new ArgumentNullException(nameof(newOperation), "cant be null");
            }
            await _context.SaveChangesAsync();

            return newOperation.Entity;
        }
        public async Task<FinancialOperation?> UpdateOperationAsync(FinancialOperation? operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation), "cant be null");
            }

            var updatedOperation = _context.FinancialOperations?.Update(operation);
            if (updatedOperation == null)
            {
                throw new ArgumentNullException(nameof(updatedOperation), "cant be null");
            }
            await _context.SaveChangesAsync();

            return updatedOperation.Entity;
        }
        public async Task<bool> DeleteOperationAsync(int operationId)
        {
            if (operationId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(operationId), "cant be less than 0");
            }

            var operationForDelete = _context.FinancialOperations?.FirstOrDefault(x => x.OperationId == operationId);

            if (operationForDelete == null)
            {
                return false;
            }

            _context.FinancialOperations?.Remove(operationForDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<FinancialOperation?> GetAllOperations()
        {
            var allOperations = _context.FinancialOperations;
            if (allOperations == null)
            {
                throw new ArgumentNullException(nameof(allOperations), "Must exist");
            }

            return allOperations.AsQueryable();
        }

        public FinancialOperation? GetOperationById(int operationId)
        {
            if (operationId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(operationId), "cant be less than 0");
            }

            var operationById = _context.FinancialOperations?.FirstOrDefault(x => x.OperationId == operationId);
            if (operationById == null)
            {
                throw new ArgumentNullException(nameof(operationById), "Must exist");
            }
            
            return operationById;
        }
    }
}
