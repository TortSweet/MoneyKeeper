using FinanceKeeper.Controllers;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FinanceKeeperTests.Controllers
{
    [TestClass()]
    public class MoneyReportControllerTests
    {
        private readonly MoneyReportController _sut;
        private readonly Mock<IMoneyReport> _servicesMock = new();
        private readonly Mock<ILogger<MoneyReportController>> _logger = new();

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

        public MoneyReportControllerTests()
        {
            this._sut = new MoneyReportController(_servicesMock.Object, _logger.Object);
        }

        [TestMethod()]
        public void GetDayReportNullTest()
        {
            var result = _sut.GetDayReport(null);

            Assert.AreEqual(result.Result?.ToString(), "Microsoft.AspNetCore.Mvc.BadRequestResult");
        }

        [TestMethod()]
        public void GetDayReportTest()
        {
            var report = new MoneyReport()
            {
                TotalExpense = 5000,
                TotalIncome = 7000,
                ListOfOperations = new List<FinancialOperation>()
                {
                    _operation1,
                    _operation2,
                    _operation3
                }
            };

            _servicesMock.Setup(x => x.GetMoneyReportByDay(It.IsAny<string>())).Returns(report);
            var result = _sut.GetDayReport("01.01.2022");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result?.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }

        [TestMethod()]
        public void GetPeriodReportNullTest()
        {
            var result = _sut.GetPeriodReport(null, null);

            Assert.AreEqual(result.Result?.ToString(), "Microsoft.AspNetCore.Mvc.BadRequestResult");
        }

        [TestMethod()]
        public void GetPeriodReportTest()
        {
            var report = new MoneyReport()
            {
                TotalExpense = 5000,
                TotalIncome = 7000,
                ListOfOperations = new List<FinancialOperation>()
                {
                    _operation1,
                    _operation2,
                    _operation3
                }
            };

            _servicesMock.Setup(x => 
                x.GetMoneyReportForThePeriod(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(report);
            var result = _sut.GetDayReport("01.01.2022");

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result?.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }
    }
}