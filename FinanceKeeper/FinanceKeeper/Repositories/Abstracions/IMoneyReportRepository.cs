using FinanceKeeper.Data.Entities;

namespace FinanceKeeper.Repositories.Abstracions
{
    public interface IMoneyReportRepository
    {
        Task<MoneyReport> GetMoneyReportByDayAsync(DateTime date);
        Task<MoneyReport> GetMoneyReportForThePeriodAsync(DateTime startDate, DateTime finalDate);
    }
}
