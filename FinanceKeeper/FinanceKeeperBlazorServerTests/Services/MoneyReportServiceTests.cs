using FinanceKeeperBlazorServer.Data.Models;
using FinanceKeeperBlazorServer.Services;
using FinanceKeeperBlazorServer.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FinanceKeeperBlazorServerTests.Services
{
    [TestClass()]
    public class MoneyReportServiceTests
    {
        private readonly Mock<IFinanceKeeperApi?> _refitMock = new();
        private readonly MoneyReportService _sut;

        public MoneyReportServiceTests()
        {
            _sut = new MoneyReportService(_refitMock.Object);
        }

        [TestMethod()]
        public void CreateEntryNullConstructorAsyncTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new MoneyReportService(null));
        }

        [TestMethod()]
        public void GetReportByDateAsyncTest()
        {
            var test = new MoneyReport()
            {
                ListOfOperations = new List<FinancialOperation>()
                {
                    new FinancialOperation()
                    {
                        FinancialCategoryId = 1,
                        Date = new DateTime(2022, 12, 15),
                        Amount = 5000,
                        Description = "test",
                        OperationId = 1
                    },
                    new FinancialOperation()
                    {
                        FinancialCategoryId = 2,
                        Date = new DateTime(2022, 12, 10),
                        Amount = 10000,
                        Description = "test 2",
                        OperationId = 2
                    }
                },
                TotalExpense = 5000,
                TotalIncome = 10000
            };
            _refitMock.Setup(x => x!.ReportPerDay(It.IsAny<string>()))
                .Returns(Task.FromResult(test));

            var result = _sut.GetReportByDateAsync(new DateTime(2022, 07, 10));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result.TotalExpense, test.TotalExpense);
        }
        
        [TestMethod()]
        public void GetReportByPeriodAsyncTest()
        {
            var test = new MoneyReport()
            {
                ListOfOperations = new List<FinancialOperation>()
                {
                    new FinancialOperation()
                    {
                        FinancialCategoryId = 1,
                        Date = new DateTime(2022, 12, 15),
                        Amount = 5000,
                        Description = "test",
                        OperationId = 1
                    },
                    new FinancialOperation()
                    {
                        FinancialCategoryId = 2,
                        Date = new DateTime(2022, 12, 10),
                        Amount = 10000,
                        Description = "test 2",
                        OperationId = 2
                    }
                },
                TotalExpense = 5000,
                TotalIncome = 10000
            };
            _refitMock.Setup(x => x!.ReportPerPeriod(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(test));

            var result = _sut.GetReportByPeriodAsync(new DateTime(2022, 07, 01), new DateTime(2022, 07, 20));

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result.TotalExpense, test.TotalExpense);
        }

        [TestMethod()]
        public void GetReportByDateInvalidDataAsyncTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.GetReportByDateAsync(new DateTime(1,1,1)));
        }
        [TestMethod()]
        public void GetReportByPeriodInvalidDataAsyncTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => 
                _sut.GetReportByPeriodAsync(new DateTime(1, 1, 1), new DateTime(2200, 12, 31)));
        }
    }
}