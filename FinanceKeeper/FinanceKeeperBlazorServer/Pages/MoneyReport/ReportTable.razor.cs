using Microsoft.AspNetCore.Components;

namespace FinanceKeeperBlazorServer.Pages.MoneyReport
{

    public partial class ReportTable : ComponentBase
    {
        [Parameter] public Data.Models.MoneyReport Report { get; set; } = null!;
    }
}
