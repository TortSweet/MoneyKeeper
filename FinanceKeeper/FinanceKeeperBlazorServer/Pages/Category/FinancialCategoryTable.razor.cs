using FinanceKeeperBlazorServer.Data.Models;
using FinanceKeeperBlazorServer.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FinanceKeeperBlazorServer.Pages.Category
{
    public partial class FinancialCategoryTable : ComponentBase
    {
        [Inject]
        protected IBaseServices<FinancialCategory> BaseServices { get; set; } = null!;


        protected IEnumerable<FinancialCategory> Categories = null!;


        protected override async Task OnInitializedAsync()
        {
            Categories = (IEnumerable<FinancialCategory>)await BaseServices.GetAllAsync();
        }

        protected async Task Delete(int id)
        {
            await BaseServices.DeleteAsync(id);
            await OnInitializedAsync();
        }
    }
}
