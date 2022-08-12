using FinanceKeeperBlazorServer.Data.Models;
using FinanceKeeperBlazorServer.Services;
using FinanceKeeperBlazorServer.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FinanceKeeperBlazorServerTests.Services
{
    [TestClass()]
    public class FinancialOperationServiceTests
    {
        private readonly Mock<IFinanceKeeperApi?> _refitMock = new();
        private readonly FinancialOperationService _sut;

        public FinancialOperationServiceTests()
        {
            _sut = new FinancialOperationService(_refitMock.Object!);
        }

        [TestMethod()]
        public void CreateEntryNullConstructorAsyncTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FinancialOperationService(null!));
        }

        [TestMethod()]
        public void CreateAsyncTest()
        {
            var test = new FinancialOperation()
            {
                FinancialCategoryId = 1,
                Date = new DateTime(2022, 12, 15),
                Amount = 5000,
                Description = "test",
                OperationId = 1
            };

            _refitMock.Setup(x => x!.CreateOperation(It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(test));

            var result = _sut.CreateAsync(test);

            Assert.IsNotNull(() => _sut.CreateAsync(test));
        }

        [TestMethod()]
        public void DeleteAsyncTest()
        {

            _refitMock.Setup(x => x!.DeleteOperation(It.IsAny<int>()))
                .Returns(Task.FromResult(new bool()));
            var result = _sut.DeleteAsync(1);

            Assert.IsNotNull(() => _sut.DeleteAsync(1));
        }


        [TestMethod()]
        public void UpdateAsyncTest()
        {
            var test = new FinancialOperation()
            {
                FinancialCategoryId = 1,
                Date = new DateTime(2022, 12, 15),
                Amount = 5000,
                Description = "test",
                OperationId = 1
            };

            _refitMock.Setup(x => x!.UpdateOperation(It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(test));
            var result = _sut.UpdateAsync(test);

            Assert.IsNotNull(() => _sut.UpdateAsync(test));
        }

        [TestMethod()]
        public void GetAllAsyncTest()
        {
            IEnumerable<FinancialOperation> listCategory = new List<FinancialOperation>()
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
            };

            _refitMock.Setup(x => x!.GetAllOperations())
                .Returns(Task.FromResult(listCategory));

            var result = _sut.GetAllAsync();

            Assert.IsNotNull(() => _sut.GetAllAsync());
        }

        [TestMethod()]
        public void GetByIdAsyncTest()
        {
            var test = new FinancialOperation()
            {
                FinancialCategoryId = 2,
                Date = new DateTime(2022, 12, 10),
                Amount = 10000,
                Description = "test 2",
                OperationId = 2
            };

            _refitMock.Setup(x => x!.GetOperationById(It.IsAny<int>()))
                .Returns(Task.FromResult(test));

            var result = _sut.GetByIdAsync(1);

            Assert.AreEqual(result.Result.Description, test.Description);
        }

        [TestMethod()]
        public void GetByIdAsyncInvalidDataTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(() => _sut.GetByIdAsync(0));
        }
        [TestMethod()]
        public void UpdateAsyncInvalidDataTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.CreateAsync(null!));
        }
        [TestMethod()]
        public void CreateAsyncInvalidDataTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.CreateAsync(null!));
        }
        [TestMethod()]
        public void DeleteAsyncInvalidDataTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(() => _sut.GetByIdAsync(0));
        }
    }
}