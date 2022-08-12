using FinanceKeeperBlazorServer.Data.Models;
using FinanceKeeperBlazorServer.Services.Interfaces;
using System.Text;
using System.Text.Json;

namespace FinanceKeeperBlazorServer.Services
{
    public class FinancialOperationService : IBaseServices<FinancialOperation>
    {
        private readonly IFinanceKeeperApi _refitClient;

        public FinancialOperationService(IFinanceKeeperApi client)
        {
            _refitClient = client ?? throw new ArgumentNullException(nameof(client), "Must exist");
        }
        public async Task CreateAsync(FinancialOperation operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation), "Must exist");
            }
            var jsonModel = JsonSerializer.Serialize(operation);
            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            await _refitClient.CreateOperation(content);
        }

        public async Task DeleteAsync(int operationId)
        {
            if (operationId <= 0)
            {
                throw new ArgumentNullException(nameof(operationId), "Must be greater than 0");
            }
            await _refitClient.DeleteOperation(operationId);

        }

        public async Task<IEnumerable<FinancialOperation>> GetAllAsync()
        {
            var list = await _refitClient.GetAllOperations();

            return list;
        }

        public async Task<FinancialOperation> GetByIdAsync(int operationId)
        {
            if (operationId <= 0)
            {
                throw new ArgumentNullException(nameof(operationId), "Must be greater than 0");
            }
            var operation = await _refitClient.GetOperationById(operationId);

            return operation;
        }

        public async Task UpdateAsync(FinancialOperation operation)
        {
            if (operation == null)
            {
                throw new ArgumentNullException(nameof(operation), "Must exist");
            }
            var jsonModel = JsonSerializer.Serialize(operation);
            var content = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            await _refitClient.UpdateOperation(content);
        }
    }
}
