using AutoMapper;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Dtos;
using FinanceKeeper.Repositories.Abstracions;
using FinanceKeeper.Services.Abstractions;

namespace FinanceKeeper.Services
{
    public class FinancialCategoryService : IBaseServices<FinancialCategoryDto>
    {
        private readonly IFinancialCategoryRepository _financialCategoryRepository;
        private readonly IMapper _mapper;
        public FinancialCategoryService(IFinancialCategoryRepository? financialCategoryRepository, IMapper? mapper)
        {
            this._financialCategoryRepository = financialCategoryRepository ?? throw new ArgumentNullException(nameof(financialCategoryRepository), "Repository must exist");
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper must exist");
        }

        public async Task<FinancialCategoryDto> CreateEntryAsync(FinancialCategoryDto? category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category model can't be null");
            }

            var newCategory = await _financialCategoryRepository.CreateCategoryAsync(_mapper.Map<FinancialCategory>(category));

            return _mapper.Map<FinancialCategoryDto>(newCategory);
        }

        public async Task<FinancialCategoryDto> UpdateEntryAsync(FinancialCategoryDto? category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category model can't be null");
            }
            var updatedCategory = await _financialCategoryRepository.UpdateCategoryAsync(_mapper.Map<FinancialCategory>(category));
            return _mapper.Map<FinancialCategoryDto>(updatedCategory);
        }

        public async Task<bool> DeleteEntryAsync(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentOutOfRangeException("Category id must be larger than 0", nameof(categoryId));
            }

            var isDeleted = await _financialCategoryRepository.DeleteCategoryAsync(categoryId);
            return isDeleted;
        }

        public IList<FinancialCategoryDto> GetAllEntries()
        {
            var categories = _financialCategoryRepository.GetAllCategories();

            return _mapper.Map<List<FinancialCategoryDto>>(categories);
        }

        public FinancialCategoryDto GetEntryById(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentOutOfRangeException("Category id must be larger than 0", nameof(categoryId));
            }
            var categories = _financialCategoryRepository.GetCategoryById(categoryId);
            return _mapper.Map<FinancialCategoryDto>(categories);
        }
    }
}
