using FinanceKeeper.Data;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Repositories.Abstracions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FinanceKeeper.Repositories
{
    public class FinancialCategoryRepository : IFinancialCategoryRepository
    {
        private readonly AppDbContext _context;

        public FinancialCategoryRepository(AppDbContext? context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "AppDbContext can't be null");
        }

        public async Task<FinancialCategory?> CreateCategoryAsync(FinancialCategory? category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "cant be null");
            }

            var newCategory = _context.FinancialCategories?.Add(category);
            if (newCategory == null)
            {
                throw new ArgumentNullException(nameof(newCategory), "Must exist");
            }
            await _context.SaveChangesAsync();

            return newCategory.Entity;
        }
        public async Task<FinancialCategory?> UpdateCategoryAsync(FinancialCategory? category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "cant be null");
            }

            var updatedCategory = _context.FinancialCategories?.Update(category);
            if (updatedCategory == null)
            {
                throw new ArgumentNullException(nameof(updatedCategory), "Must exist");
            }
            await _context.SaveChangesAsync();

            return updatedCategory.Entity;
        }
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(categoryId), "cant be less than 0");
            }

            var categoryForDelete = _context.FinancialCategories?.FirstOrDefault(x => x.CategoryId == categoryId);

            if (categoryForDelete == null)
            {
                return false;
            }

            _context.FinancialCategories?.Remove(categoryForDelete);
            await _context.SaveChangesAsync();
            return true;
        }

        public IQueryable<FinancialCategory?> GetAllCategories()
        {
            var allCategories = _context.FinancialCategories;
            if (allCategories == null)
            {
                throw new ArgumentNullException(nameof(allCategories), "Must exist");
            }
            return allCategories.AsQueryable();
        }

        public FinancialCategory? GetCategoryById(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(categoryId), "cant be less than 0");
            }
            
            var categoryById = _context.FinancialCategories?.FirstOrDefault(x => x.CategoryId == categoryId);

            if (categoryById == null)
            {
                throw new ArgumentNullException(nameof(categoryById), "Must exist");
            }
            return categoryById;
        }
    }
}
