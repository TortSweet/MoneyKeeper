using FinanceKeeperBlazorServer.Data.Models;

namespace FinanceKeeperBlazorServer.Services.Interfaces
{
    public interface IMoneyReport
    {
        Task<MoneyReport> GetReportByDateAsync(DateTime day);
        Task<MoneyReport> GetReportByPeriodAsync(DateTime startDay, DateTime endDay);
    }
}
