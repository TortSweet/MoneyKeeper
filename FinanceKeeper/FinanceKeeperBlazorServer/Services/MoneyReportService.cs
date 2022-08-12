using FinanceKeeperBlazorServer.Data.Models;
using FinanceKeeperBlazorServer.Services.Interfaces;

namespace FinanceKeeperBlazorServer.Services
{
    public class MoneyReportService : IMoneyReport
    {
        private readonly IFinanceKeeperApi _refitClient;

        public MoneyReportService(IFinanceKeeperApi? client)
        {
            _refitClient = client ?? throw new ArgumentNullException(nameof(client), "Must exist");
        }
        public async Task<MoneyReport> GetReportByDateAsync(DateTime date)
        {
            if (new DateTime(2000, 01, 01) >= date && date >= new DateTime(2100, 01, 01))
            {
                throw new ArgumentException("Must exist in our century", nameof(date));
            }
            var stringDate = date.ToString("dd.MM.yyyy");
            var model = await _refitClient.ReportPerDay(stringDate);

            return model!;
        }

        public async Task<MoneyReport> GetReportByPeriodAsync(DateTime startDay, DateTime endDay)
        {
            if (new DateTime(2000, 01, 01) >= startDay && startDay >= new DateTime(2100, 01, 01))
            {
                throw new ArgumentException("Must exist in our century", nameof(startDay));
            }
            if (new DateTime(2000, 01, 01) >= endDay && endDay >= new DateTime(2100, 01, 01))
            {
                throw new ArgumentException("Must exist in our century", nameof(endDay));
            }

            var startDaySrt = startDay.ToString("dd.MM.yyyy");
            var endDayStr = endDay.ToString("dd.MM.yyyy");

            var model = await _refitClient.ReportPerPeriod(startDaySrt, endDayStr);

            return model!;
        }
    }
}
