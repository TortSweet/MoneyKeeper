using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceKeeper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Repositories.Abstracions;
using Moq;

namespace FinanceKeeper.Services.Tests
{
    [TestClass()]
    public class MoneyReportServiceTests
    {
        private readonly MoneyReportService _sut;
        private readonly Mock<IFinancialCategoryRepository> _repositoryCategoryMock = new();
        private readonly Mock<IFinancialOperationRepository> _repositoryOperationMock = new();

        public MoneyReportServiceTests()
        {
            _sut = new MoneyReportService(_repositoryCategoryMock.Object, _repositoryOperationMock.Object);
        }

        private readonly FinancialCategory _categoryIncome = new()
        {
            Type = FinanceType.Expense,
            CategoryId = 1,
            Name = "Test expense"
        };
        private readonly FinancialCategory _categoryExpense = new()
        {
            Type = FinanceType.Income,
            CategoryId = 2,
            Name = "Test income"
        };
        private readonly FinancialOperation _operation1 = new()
        {
            FinancialCategoryId = 1,
            Date = new DateTime(2022, 03, 05),
            Amount = 5000,
            Description = "Test description 1",
            OperationId = 1
        };
        private readonly FinancialOperation _operation2 = new()
        {
            FinancialCategoryId = 1,
            Date = new DateTime(2022, 03, 08),
            Amount = 1111,
            Description = "Test description 2",
            OperationId = 2
        };
        private readonly FinancialOperation _operation3 = new()
        {
            FinancialCategoryId = 2,
            Date = new DateTime(2022, 03, 07),
            Amount = 2500,
            Description = "Test description 2",
            OperationId = 3
        };

        [TestMethod()]
        public void GetMoneyReportByDayTest()
        {
            var categoriesList = new List<FinancialCategory>()
            {
                _categoryExpense,
                _categoryIncome
            };
            var operationsList = new List<FinancialOperation>()
            {
                _operation1,
                _operation2,
                _operation3
            };

            _repositoryCategoryMock.Setup(x => x.GetAllCategories()).Returns(categoriesList.AsQueryable);
            _repositoryOperationMock.Setup(x => x.GetAllOperations()).Returns(operationsList.AsQueryable);

            var result = _sut.GetMoneyReportByDay("07.03.2022");

            Assert.IsNotNull(result);

            var moneyReport = new MoneyReport()
            {
                ListOfOperations = new List<FinancialOperation>() { _operation3 },
                TotalExpense = 0,
                TotalIncome = 2500
            };
            Assert.AreEqual(result.TotalExpense, moneyReport.TotalExpense);
            Assert.AreEqual(result.TotalIncome, moneyReport.TotalIncome);
            Assert.AreEqual(result.ListOfOperations?.Count, moneyReport.ListOfOperations.Count);
        }

        [TestMethod()]
        public void GetMoneyReportForThePeriodTest()
        {
            var categoriesList = new List<FinancialCategory>()
            {
                _categoryExpense,
                _categoryIncome
            };
            var operationsList = new List<FinancialOperation>()
            {
                _operation1,
                _operation2,
                _operation3
            };
            _repositoryCategoryMock.Setup(x => x.GetAllCategories()).Returns(categoriesList.AsQueryable);
            _repositoryOperationMock.Setup(x => x.GetAllOperations()).Returns(operationsList.AsQueryable);

            var result = _sut.GetMoneyReportForThePeriod("01.03.2022", "10.03.2022");

            Assert.IsNotNull(result);

            var moneyReport = new MoneyReport()
            {
                ListOfOperations = new List<FinancialOperation>() { _operation1, _operation2, _operation3 },
                TotalExpense = 6111,
                TotalIncome = 2500
            };
            Assert.AreEqual(result.TotalExpense, moneyReport.TotalExpense);
            Assert.AreEqual(result.TotalIncome, moneyReport.TotalIncome);
            Assert.AreEqual(result.ListOfOperations?.Count, moneyReport.ListOfOperations.Count);
        }
    }
}