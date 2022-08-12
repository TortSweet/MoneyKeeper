using FinanceKeeper.Data;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Repositories;
using FinanceKeeper.Repositories.Abstracions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinanceKeeperTests.Repositories
{
    [TestClass()]
    public class FinancialOperationRepositoryTests
    {
        private readonly IFinancialOperationRepository _sut = GetInMemoryFinancialRepository();

        private static IFinancialOperationRepository GetInMemoryFinancialRepository()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;

            AppDbContext? operationDataContext = new AppDbContext(options);
            operationDataContext.Database.EnsureDeleted();
            operationDataContext.Database.EnsureCreated();
            return new FinancialOperationRepository(operationDataContext);

        }

        private readonly FinancialOperation? _testOperation1 = new FinancialOperation()
        {
            FinancialCategoryId = 1,
            Date = new DateTime(2022, 07, 03),
            Amount = 5000,
            Description = "Test description",
            OperationId = 1
        };

        private readonly FinancialOperation _testOperation2 = new FinancialOperation()
        {
            FinancialCategoryId = 2,
            Date = new DateTime(2022, 07, 03),
            Amount = 10000,
            Description = "Test description 2",
            OperationId = 1
        };

        private readonly FinancialOperation? _testOperation3 = new FinancialOperation()
        {
            FinancialCategoryId = 2,
            Date = new DateTime(2022, 07, 03),
            Amount = 1111,
            Description = "Test description 3",
            OperationId = 2
        };

        #region MyRegion

        [TestMethod]
        public void OperationConstructorTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FinancialOperationRepository(null));
        }

        [TestMethod()]
        public void CreateOperationAsyncTest()
        {
            var writeOperation = _sut.CreateOperationAsync(_testOperation1);

            Assert.IsNotNull(writeOperation);
            Assert.AreEqual(writeOperation.Result?.FinancialCategoryId, _testOperation1?.FinancialCategoryId);
            Assert.AreEqual(writeOperation.Result?.Amount, _testOperation1?.Amount);
            Assert.AreEqual(writeOperation.Result?.OperationId, _testOperation1?.OperationId);
        }

        [TestMethod()]
        public void CreateOperationAsyncNullTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.CreateOperationAsync(null));
        }

        [TestMethod()]
        public async Task UpdateOperationAsyncTestAsync()
        {
            var writeOperation = await this._sut.CreateOperationAsync(_testOperation1);
            writeOperation!.FinancialCategoryId = _testOperation2.FinancialCategoryId;
            writeOperation.Amount = _testOperation2.Amount;
            writeOperation.Date = _testOperation2.Date;
            writeOperation.Description = _testOperation2.Description;
            writeOperation.OperationId = _testOperation2.OperationId;


            Console.WriteLine();


            Assert.IsNotNull(writeOperation);
            Assert.AreEqual(writeOperation.FinancialCategoryId, _testOperation2.FinancialCategoryId);
            Assert.AreEqual(writeOperation.OperationId, _testOperation2.OperationId);
            Assert.AreEqual(writeOperation.Amount, _testOperation2.Amount);
        }

        [TestMethod()]
        public void UpdateOperationAsyncNullTestAsync()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.UpdateOperationAsync(null));
        }

        [TestMethod()]
        public void DeleteOperationAsyncInvalidDataTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(() => _sut.DeleteOperationAsync(0));
        }

        [TestMethod()]
        public void DeleteOperationAsyncTest()
        {
            var writeOperation = _sut.CreateOperationAsync(_testOperation1);
            var resNotExistId = _sut.DeleteOperationAsync(55);
            Assert.AreEqual(resNotExistId.Result, false);

            var res = _sut.DeleteOperationAsync(1);
            Assert.AreEqual(res.Result, true);
        }

        #endregion

        [TestMethod()]
        public void GetAllOperationsAsyncTest()
        {
            _sut.CreateOperationAsync(_testOperation1);
            _sut.CreateOperationAsync(_testOperation3);

            var result = _sut.GetAllOperations();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
        }

        [TestMethod()]
        public void GetOperationsByIdAsyncInvalidDataTest()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _sut.GetOperationById(0));
        }

        [TestMethod()]
        public void GetOperationByIdAsyncTest()
        {
            var writeOperation1 = _sut.CreateOperationAsync(_testOperation1);
            var writeOperation2 = _sut.CreateOperationAsync(_testOperation3);

            var result = _sut.GetOperationById(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, _testOperation3);

        }
    }
}