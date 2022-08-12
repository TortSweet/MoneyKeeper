using FinanceKeeper.Data;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Repositories;
using FinanceKeeper.Repositories.Abstracions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FinanceKeeperTests.Repositories
{
    [TestClass()]
    public class FinancialCategoryRepositoryTests
    {
        private readonly IFinancialCategoryRepository _sut = GetInMemoryCategoryRepository();

        private static IFinancialCategoryRepository GetInMemoryCategoryRepository()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            options = builder.Options;

            AppDbContext? categoryDataContext = new AppDbContext(options);
            categoryDataContext.Database.EnsureDeleted();
            categoryDataContext.Database.EnsureCreated();
            return new FinancialCategoryRepository(categoryDataContext);

        }

        private readonly FinancialCategory? _testCategory1 = new FinancialCategory()
        {
            Type = FinanceType.Income,
            CategoryId = 1,
            Name = "Test category 1"
        };
        private readonly FinancialCategory _testCategory2 = new FinancialCategory()
        {
            Type = FinanceType.Expense,
            CategoryId = 1,
            Name = "Updated category"
        };
        private readonly FinancialCategory? _testCategory3 = new FinancialCategory()
        {
            Type = FinanceType.Expense,
            CategoryId = 2,
            Name = "Test category 2"
        };

        #region MyRegion
        [TestMethod]
        public void CategoryConstructorTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FinancialCategoryRepository(null));
        }

        [TestMethod()]
        public void CreateCategoryAsyncTest()
        {
            var writeCategory = _sut.CreateCategoryAsync(_testCategory1);

            Assert.IsNotNull(writeCategory);
            Assert.AreEqual(writeCategory.Result?.Type, _testCategory1?.Type);
            Assert.AreEqual(writeCategory.Result?.CategoryId, _testCategory1?.CategoryId);
            Assert.AreEqual(writeCategory.Result?.Name, _testCategory1?.Name);
        }
        [TestMethod()]
        public void CreateCategoryAsyncNullTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.CreateCategoryAsync(null));
        }

        [TestMethod()]
        public async Task UpdateCategoryAsyncTestAsync()
        {
            var writeCategory = await this._sut.CreateCategoryAsync(_testCategory1);

            writeCategory!.Type = _testCategory2.Type;
            writeCategory.CategoryId = _testCategory2.CategoryId;
            writeCategory.Name = _testCategory2.Name;


            Console.WriteLine();


            Assert.IsNotNull(writeCategory);
            Assert.AreEqual(writeCategory.Type, _testCategory2.Type);
            Assert.AreEqual(writeCategory.CategoryId, _testCategory2.CategoryId);
            Assert.AreEqual(writeCategory.Name, _testCategory2.Name);
        }
        [TestMethod()]
        public void UpdateCategoryAsyncNullTestAsync()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.UpdateCategoryAsync(null));
        }

        [TestMethod()]
        public void DeleteCategoryAsyncInvalidDataTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(() => _sut.DeleteCategoryAsync(0));
        }
        [TestMethod()]
        public void DeleteCategoryAsyncTest()
        {
            var writeCategory = _sut.CreateCategoryAsync(_testCategory1);
            var resNotExistId = _sut.DeleteCategoryAsync(55);
            Assert.AreEqual(resNotExistId.Result, false);

            var res = _sut.DeleteCategoryAsync(1);
            Assert.AreEqual(res.Result, true);
        }
        #endregion

        [TestMethod()]
        public void GetAllCategoriesAsyncTest()
        {
            _sut.CreateCategoryAsync(_testCategory1);
            _sut.CreateCategoryAsync(_testCategory3);

            var result = _sut.GetAllCategories();
            
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
        }

        [TestMethod()]
        public void GetCategoryByIdAsyncInvalidDataTest()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _sut.GetCategoryById(0));
        }
        [TestMethod()]
        public void GetCategoryByIdAsyncTest()
        {
            var writeCategory1 = _sut.CreateCategoryAsync(_testCategory1);
            var writeCategory2 = _sut.CreateCategoryAsync(_testCategory3);

            var result = _sut.GetCategoryById(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(result, _testCategory3);
        }
    }
}


