using FinanceKeeper.Data.Entities;

namespace FinanceKeeper.Services.Abstractions
{
    public interface IMoneyReport
    {
        MoneyReport? GetMoneyReportByDay(string? date);
        MoneyReport? GetMoneyReportForThePeriod(string? startDate, string? finalDate);
    }
}
