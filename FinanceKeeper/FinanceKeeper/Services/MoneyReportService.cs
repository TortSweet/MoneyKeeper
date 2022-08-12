using System.Globalization;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Repositories.Abstracions;
using FinanceKeeper.Services.Abstractions;

namespace FinanceKeeper.Services;

public class MoneyReportService : IMoneyReport
{
    private readonly IFinancialCategoryRepository _categoryRepository;
    private readonly IFinancialOperationRepository _operationRepository;

    public MoneyReportService(IFinancialCategoryRepository categoryRepository, IFinancialOperationRepository operationRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository), "Repository must exist");
        _operationRepository = operationRepository ?? throw new ArgumentNullException(nameof(operationRepository), "Repository must exist");
    }

    public MoneyReport? GetMoneyReportByDay(string? date)
    {
        if (string.IsNullOrWhiteSpace(date))
        {
            throw new ArgumentNullException(nameof(date), "Must exist");
        }

        var reportPerDay = GetReport(date);

        return reportPerDay;
    }

    public MoneyReport? GetMoneyReportForThePeriod(string? startDate, string? finalDate)
    {
        if (string.IsNullOrWhiteSpace(startDate))
        {
            throw new ArgumentNullException(nameof(startDate), "Must exist");
        }
        if (string.IsNullOrWhiteSpace(finalDate))
        {
            throw new ArgumentNullException(nameof(finalDate), "Must exist");
        }
        var reportByPeriod = GetReport(startDate, finalDate);

        return reportByPeriod;
    }

    #region Help method

    private MoneyReport? GetReport(string? date)
    {
        if (string.IsNullOrEmpty(date))
        {
            throw new ArgumentNullException(nameof(date), "Can't be null");
        }
        var query = (from operations in _operationRepository.GetAllOperations()
                     join category in _categoryRepository.GetAllCategories() on operations.FinancialCategoryId equals category
                         .CategoryId
                     where operations.Date == DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture)
                     select new
                     {
                         Description = operations.Description,
                         OperationId = operations.OperationId,
                         Amount = operations.Amount,
                         Date = operations.Date,
                         FinancialCategoryId = operations.FinancialCategoryId,
                         TypeOperation = category.Type
                     });

        List<FinancialOperation> operationList = new();

        foreach (var item in query)
        {
            operationList.Add(new FinancialOperation()
            {
                FinancialCategoryId = item.FinancialCategoryId,
                Date = item.Date,
                Amount = item.Amount,
                Description = item.Description,
                OperationId = item.OperationId
            });
        }

        var reportPerDay = new MoneyReport()
        {
            ListOfOperations = operationList,
            TotalExpense = query.Where(x => x.TypeOperation == FinanceType.Expense).Sum(q => q.Amount),
            TotalIncome = query.Where(x => x.TypeOperation == FinanceType.Income).Sum(q => q.Amount)
        };
        return reportPerDay;

    }

    private MoneyReport? GetReport(string? startDate, string? endDate)
    {
        if (string.IsNullOrEmpty(startDate))
        {
            throw new ArgumentNullException(nameof(startDate), "Can't be null");
        }

        if (string.IsNullOrEmpty(endDate))
        {
            throw new ArgumentNullException(nameof(endDate), "Can't be null");
        }

        var query = (from operations in _operationRepository.GetAllOperations()
                     join category in _categoryRepository.GetAllCategories() on operations.FinancialCategoryId equals category
                         .CategoryId
                     where (DateTime.ParseExact(startDate, "dd.MM.yyyy", CultureInfo.InvariantCulture) <= operations.Date && operations.Date <= DateTime.ParseExact(endDate, "dd.MM.yyyy", CultureInfo.InvariantCulture))
                     select new
                     {
                         Description = operations.Description,
                         OperationId = operations.OperationId,
                         Amount = operations.Amount,
                         Date = operations.Date,
                         FinancialCategoryId = operations.FinancialCategoryId,
                         TypeOperation = category.Type
                     });

        List<FinancialOperation> operationList = new();

        foreach (var item in query)
        {
            operationList.Add(new FinancialOperation()
            {
                FinancialCategoryId = item.FinancialCategoryId,
                Date = item.Date,
                Amount = item.Amount,
                Description = item.Description,
                OperationId = item.OperationId
            });
        }

        var reportPerDay = new MoneyReport()
        {
            ListOfOperations = operationList,
            TotalExpense = query.Where(x => x.TypeOperation == FinanceType.Expense).Sum(q => q.Amount),
            TotalIncome = query.Where(x => x.TypeOperation == FinanceType.Income).Sum(q => q.Amount)
        };
        return reportPerDay;
    }
    #endregion
}