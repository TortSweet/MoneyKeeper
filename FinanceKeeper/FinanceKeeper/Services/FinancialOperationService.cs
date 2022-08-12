using AutoMapper;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Dtos;
using FinanceKeeper.Repositories.Abstracions;
using FinanceKeeper.Services.Abstractions;

namespace FinanceKeeper.Services
{
    public class FinancialOperationService : IBaseServices<FinancialOperationDto>
    {
        private readonly IFinancialOperationRepository _financialOperationRepository;
        private readonly IMapper _mapper;
        public FinancialOperationService(IFinancialOperationRepository? financialOperationRepository, IMapper? mapper)
        {
            this._financialOperationRepository = financialOperationRepository ?? throw new ArgumentNullException(nameof(financialOperationRepository), "Repository must exist");
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "Mapper must exist");
        }

        public async Task<FinancialOperationDto> CreateEntryAsync(FinancialOperationDto? operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation), "Category model can't be null");
            }

            var newOperation = await _financialOperationRepository.CreateOperationAsync(_mapper.Map<FinancialOperation>(operation));

            return _mapper.Map<FinancialOperationDto>(newOperation);
        }

        public async Task<FinancialOperationDto> UpdateEntryAsync(FinancialOperationDto? operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation), "Category model can't be null");
            }
            var updatedOperation = await _financialOperationRepository.UpdateOperationAsync(_mapper.Map<FinancialOperation>(operation));
            return _mapper.Map<FinancialOperationDto>(updatedOperation);
        }

        public async Task<bool> DeleteEntryAsync(int operationId)
        {
            if (operationId <= 0)
            {
                throw new ArgumentOutOfRangeException("Category id must be larger than 0", nameof(operationId));
            }

            var isDeleted = await _financialOperationRepository.DeleteOperationAsync(operationId);
            return isDeleted;
        }

        public IList<FinancialOperationDto> GetAllEntries()
        {
            var operations = _financialOperationRepository.GetAllOperations();

            return _mapper.Map<List<FinancialOperationDto>>(operations);
        }

        public  FinancialOperationDto GetEntryById(int operationId)
        {
            if (operationId <= 0)
            {
                throw new ArgumentOutOfRangeException("Category id must be larger than 0", nameof(operationId));
            }
            var operation = _financialOperationRepository.GetOperationById(operationId);
            return _mapper.Map<FinancialOperationDto>(operation);
        }
    }
}

