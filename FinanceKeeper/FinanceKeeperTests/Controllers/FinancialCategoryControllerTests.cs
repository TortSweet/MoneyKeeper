using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceKeeper.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Dtos;
using FinanceKeeper.Repositories.Abstracions;
using FinanceKeeper.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FinanceKeeper.Controllers.Tests
{
    [TestClass()]
    public class FinancialCategoryControllerTests
    {
        private readonly FinancialCategoryController _sut;
        private readonly Mock<IBaseServices<FinancialCategoryDto>> _servicesMock = new();
        private readonly Mock<ILogger<FinancialCategoryController>> _logger = new();

        private readonly FinancialCategoryDto _categoryIncome = new()
        {
            Type = FinanceType.Expense,
            CategoryId = 1,
            Name = "Test expense"
        };
        private readonly FinancialCategoryDto _categoryExpense = new()
        {
            Type = FinanceType.Income,
            CategoryId = 2,
            Name = "Test income"
        };

        public FinancialCategoryControllerTests()
        {
            _sut = new FinancialCategoryController(_servicesMock.Object, _logger.Object);
        }

        [TestMethod()]
        public void CreateCategoryNullTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.CreateCategoryAsync(null));
        }
        [TestMethod()]
        public async Task CreateCategoryAsyncTest()
        {
            var newDto = new FinancialCategoryDto()
            {
                Type = FinanceType.Expense,
                CategoryId = 1,
                Name = "Test"
            };

            _servicesMock.Setup(x =>
                    x.CreateEntryAsync(It.IsAny<FinancialCategoryDto>()))
                .ReturnsAsync(newDto);

            var result = await _sut.CreateCategoryAsync(newDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result?.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }

        [TestMethod()]
        public void UpdateCategoryAsyncNullTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.UpdateCategoryAsync(null));
        }
        [TestMethod()]
        public async Task UpdateCategoryAsyncTest()
        {
            var newDto = new FinancialCategoryDto()
            {
                Type = FinanceType.Expense,
                CategoryId = 1,
                Name = "Test"
            };

            _servicesMock.Setup(x =>
                    x.UpdateEntryAsync(It.IsAny<FinancialCategoryDto>()))
                .ReturnsAsync(newDto);

            var result = await _sut.UpdateCategoryAsync(newDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result?.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }
        [TestMethod()]
        public void DeleteCategoryAsyncNullTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(() => _sut.DeleteCategoryAsync(0));
        }
        [TestMethod()]
        public async Task DeleteCategoryAsyncTest()
        {

            _servicesMock.Setup(x =>
                    x.DeleteEntryAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            var result = await _sut.DeleteCategoryAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }

        [TestMethod()]
        public void GetAllCategoriesTest()
        {
            var categoryList = new List<FinancialCategoryDto>()
            {
                _categoryExpense,
                _categoryIncome
            };
            _servicesMock.Setup(x => x.GetAllEntries()).Returns(categoryList);


            var result = _sut.GetAllCategories();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }

        [TestMethod()]
        public void GetCategoryByIdInvalidDataTest()
        {
            var result = _sut.GetCategoryById(0);

            Assert.AreEqual(result.Result?.ToString(), "Microsoft.AspNetCore.Mvc.BadRequestResult");
        }
        [TestMethod()]
        public void GetCategoryByIdTest()
        {
            _servicesMock.Setup(x => x.GetEntryById(It.IsAny<int>()))
                .Returns(_categoryExpense);

            var result = _sut.GetCategoryById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Result.ToString(), "Microsoft.AspNetCore.Mvc.OkObjectResult");
        }
    }
}