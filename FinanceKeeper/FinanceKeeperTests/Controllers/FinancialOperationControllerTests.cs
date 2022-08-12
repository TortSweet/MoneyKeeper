using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceKeeper.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Dtos;
using FinanceKeeper.Services.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;

namespace FinanceKeeper.Controllers.Tests
{
    [TestClass()]
    public class FinancialOperationControllerTests
    {
        private readonly FinancialOperationController _sut;
        private readonly Mock<IBaseServices<FinancialOperationDto?>> _servicesMock = new();
        private readonly Mock<ILogger<FinancialOperationController>> _logger = new();

        private readonly FinancialOperationDto _operation1 = new()
        {
            FinancialCategoryId = 1,
            Date = new DateTime(2022, 03, 05),
            Amount = 5000,
            Description = "Test description 1",
            OperationId = 1
        };
        private readonly FinancialOperationDto _operation2 = new()
        {
            FinancialCategoryId = 1,
            Date = new DateTime(2022, 03, 08),
            Amount = 1111,
            Description = "Test description 2",
            OperationId = 2
        };

        public FinancialOperationControllerTests()
        {
            _sut = new FinancialOperationController(_servicesMock.Object, _logger.Object);
        }

        [TestMethod()]
        public void CreateOperationNullTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.CreateOperationAsync(null));
        }
        [TestMethod()]
        public async Task CreateOperationAsyncTest()
        {
            var newDto = new FinancialOperationDto()
            {
                FinancialCategoryId = 1,
                Date = new DateTime(2022, 05, 15),
                Amount = 1111,
                Description = "Test desc",
                OperationId = 1
            };

            _servicesMock.Setup(x =>
                    x.CreateEntryAsync(It.IsAny<FinancialOperationDto>()))
                .ReturnsAsync(newDto);

            var result = await _sut.CreateOperationAsync(newDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result?.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }

        [TestMethod()]
        public void UpdateOperationAsyncNullTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.UpdateOperationAsync(null));
        }
        [TestMethod()]
        public async Task UpdateCategoryAsyncTest()
        {
            var newDto = new FinancialOperationDto()
            {
                FinancialCategoryId = 1,
                Date = new DateTime(2022, 05, 15),
                Amount = 1111,
                Description = "Test desc",
                OperationId = 1
            };

            _servicesMock.Setup(x =>
                    x.UpdateEntryAsync(It.IsAny<FinancialOperationDto>()))
                .ReturnsAsync(newDto);

            var result = await _sut.UpdateOperationAsync(newDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result?.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }
        [TestMethod()]
        public void DeleteOperationAsyncNullTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(() => _sut.DeleteOperationAsync(0));
        }
        [TestMethod()]
        public async Task DeleteOperationAsyncTest()
        {

            _servicesMock.Setup(x =>
                    x.DeleteEntryAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var result = await _sut.DeleteOperationAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }

        [TestMethod()]
        public void GetAllOperationTest()
        {
            var operationList = new List<FinancialOperationDto>()
            {
                _operation1,
                _operation2
            };
            _servicesMock.Setup(x => x.GetAllEntries()).Returns(operationList!);


            var result = _sut.GetAllOperations();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }

        [TestMethod()]
        public void GetOperationByIdInvalidDataTest()
        {
            var result = _sut.GetOperationById(0);

            Assert.AreEqual(result.Result?.ToString(), "Microsoft.AspNetCore.Mvc.BadRequestResult");
        }
        [TestMethod()]
        public void GetOperationByIdTest()
        {
            _servicesMock.Setup(x => x.GetEntryById(It.IsAny<int>()))
                .Returns(_operation1);

            var result = _sut.GetOperationById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result?.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }
    }
}