using FinanceKeeperBlazorServer.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace FinanceKeeperBlazorServer.Pages.MoneyReport
{
    public partial class DayReport
    {
        [Inject] protected IMoneyReport BaseService { get; set; } = null!;
        protected DateTime DateFrom { get; set; }

        protected Data.Models.MoneyReport Report = null!;
        protected bool ShowTable = false;

        protected async Task GetReport()
        {
            Report = await BaseService.GetReportByDateAsync(DateFrom);
            ShowTable = true;
        }
    }
}
