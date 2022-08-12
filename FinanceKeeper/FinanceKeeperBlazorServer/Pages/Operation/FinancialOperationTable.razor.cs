using FinanceKeeperBlazorServer.Data.Models;
using FinanceKeeperBlazorServer.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FinanceKeeperBlazorServer.Pages.Operation
{
    public partial class FinancialOperationTable : ComponentBase
    {
        [Inject]
        protected IBaseServices<FinancialOperation> BaseServices { get; set; } = null!;


        protected IEnumerable<FinancialOperation> Operations = null!;


        protected override async Task OnInitializedAsync()
        {
            Operations = (IEnumerable<FinancialOperation>)await BaseServices.GetAllAsync();
        }

        protected async Task Delete(int id)
        {
            await BaseServices.DeleteAsync(id);
            await OnInitializedAsync();
        }
    }
}
