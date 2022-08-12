using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceKeeper.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FinanceKeeper.AutoMapperProfiles;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Dtos;
using FinanceKeeper.Repositories.Abstracions;
using Moq;

namespace FinanceKeeper.Services.Tests
{
    [TestClass()]
    public class FinancialOperationServiceTests
    {
        private readonly FinancialOperationService _sut;
        private readonly Mock<IFinancialOperationRepository?> _repositoryMock = new();
        private static MapperConfiguration config = new(x => x
            .AddProfile(new FinancialOperationProfile()));
        private IMapper? _mapper = new Mapper(config);

        public FinancialOperationServiceTests()
        {
            _sut = new FinancialOperationService(_repositoryMock.Object, _mapper);
        }
        private readonly FinancialOperation operation = new FinancialOperation()
        {
            FinancialCategoryId = 1,
            Date = new DateTime(2022, 03, 22),
            Amount = 5000,
            Description = "Test description",
            OperationId = 1
        };
        private readonly FinancialOperationDto? operationDto = new FinancialOperationDto()
        {
            FinancialCategoryId = 1,
            Date = new DateTime(2022, 03, 22),
            Amount = 5000,
            Description = "Test description",
            OperationId = 1
        };


        [TestMethod()]
        public void CreateEntryNullConstructorAsyncTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FinancialOperationService(null, null));
        }

        [TestMethod()]
        public void CreateEntryAsyncNullTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.CreateEntryAsync(null));
        }
        [TestMethod()]
        public void CreateEntryAsyncTest()
        {
            _repositoryMock.Setup(x
                => x!.CreateOperationAsync(It.IsAny<FinancialOperation>())).Returns(Task.FromResult(operation)!);

            var result = _sut.CreateEntryAsync(operationDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(operationDto?.FinancialCategoryId, result.Result.FinancialCategoryId);
            Assert.AreEqual(operationDto?.Amount, result.Result.Amount);
            Assert.AreEqual(operationDto?.OperationId, result.Result.OperationId);
        }

        [TestMethod()]
        public void UpdateEntryAsyncNullTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.UpdateEntryAsync(null));
        }
        [TestMethod()]
        public void UpdateEntryAsyncTest()
        {
            _repositoryMock.Setup(x
                => x!.UpdateOperationAsync(It.IsAny<FinancialOperation>())).Returns(Task.FromResult(operation)!);

            var result = _sut.UpdateEntryAsync(operationDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(operationDto?.FinancialCategoryId, result.Result.FinancialCategoryId);
            Assert.AreEqual(operationDto?.Amount, result.Result.Amount);
            Assert.AreEqual(operationDto?.OperationId, result.Result.OperationId);
        }
        [TestMethod()]
        public void DeleteEntryAsyncNullTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(() => _sut.DeleteEntryAsync(0));
        }
        [TestMethod()]
        public void DeleteEntryAsyncTest()
        {
            _repositoryMock.Setup(x =>
                x!.DeleteOperationAsync(It.IsAny<int>())).Returns(Task.FromResult(true));

            var resultTrue = _sut.DeleteEntryAsync(1);

            Assert.AreEqual(resultTrue.Result, true);

            _repositoryMock.Setup(x =>
                x!.DeleteOperationAsync(It.IsAny<int>())).Returns(Task.FromResult(false));

            var resultFalse = _sut.DeleteEntryAsync(1);

            Assert.AreEqual(resultFalse.Result, false);
        }
        [TestMethod()]
        public void GetAllEntriesTest()
        {
            var listOperations = new List<FinancialOperation>()
            {
                operation,
                new FinancialOperation()
                {
                    FinancialCategoryId = 2,
                    Date = new DateTime(2022, 07, 07),
                    Amount = 11111,
                    Description = "Test description 2",
                    OperationId = 3
                }
            };

            _repositoryMock.Setup(x =>
                x!.GetAllOperations()).Returns(listOperations.AsQueryable);

            var result = _sut.GetAllEntries();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Test description 2", result[1].Description);
        }
        [TestMethod()]
        public void GetEntryByIdNullTest()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _sut.GetEntryById(0));
        }
        [TestMethod()]
        public void GetEntryByIdTest()
        {
            _repositoryMock.Setup(x => x!.GetOperationById(It.IsAny<int>())).Returns(operation);
            var result = _sut.GetEntryById(1);

            Assert.AreEqual(operationDto?.FinancialCategoryId, result.FinancialCategoryId);
            Assert.AreEqual(operationDto?.Amount, result.Amount);
            Assert.AreEqual(operationDto?.Date, result.Date);
        }
    }
}