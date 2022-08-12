using FinanceKeeperBlazorServer.Data.Models;
using FinanceKeeperBlazorServer.Services;
using FinanceKeeperBlazorServer.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace FinanceKeeperBlazorServerTests.Services
{
    [TestClass()]
    public class FinancialCategoryServiceTests
    {
        private readonly Mock<IFinanceKeeperApi?> _refitMock = new();
        private readonly FinancialCategoryService _sut;

        public FinancialCategoryServiceTests()
        {
            _sut = new FinancialCategoryService(_refitMock.Object!);
        }

        [TestMethod()]
        public void CreateEntryNullConstructorAsyncTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FinancialCategoryService(null!));
        }

        [TestMethod()]
        public Task CreateAsyncTest()
        {
            var test = new FinancialCategory()
            {
                Type = FinanceType.Income,
                CategoryId = 2,
                Name = "Test_2"
            };

            _refitMock.Setup(x => x!.CreateCategory(It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(test));

            var result =  _sut.CreateAsync(test);
            

            Assert.IsNotNull(() => _sut.CreateAsync(test));
            return Task.CompletedTask;
        }

        [TestMethod()]
        public void DeleteAsyncTest()
        {

            _refitMock.Setup(x => x!.DeleteCategory(It.IsAny<int>()))
                .Returns(Task.FromResult(new bool()));
            var result = _sut.DeleteAsync(1);

            Assert.IsNotNull(() => _sut.DeleteAsync(1));
        }


        [TestMethod()]
        public void UpdateAsyncTest()
        {
            var test = new FinancialCategory()
            {
                Type = FinanceType.Income,
                CategoryId = 2,
                Name = "Test_2"
            };

            _refitMock.Setup(x => x!.UpdateCategory(It.IsAny<HttpContent>()))
                .Returns(Task.FromResult(test));
            var result = _sut.UpdateAsync(test);


            Assert.IsNotNull(() => _sut.UpdateAsync(test));
        }

        [TestMethod()]
        public void GetAllAsyncTest()
        {
            IEnumerable<FinancialCategory> listCategory = new List<FinancialCategory>()
            {
                new FinancialCategory()
                {
                    Type = FinanceType.Expense,
                    CategoryId = 1,
                    Name = "Test_1"
                },
                new FinancialCategory()
                {
                    Type = FinanceType.Income,
                    CategoryId = 2,
                    Name = "Test_2"
                }
            };

            _refitMock.Setup(x => x!.GetCategories())
                .Returns(Task.FromResult(listCategory));

            var result = _sut.GetAllAsync();

            Assert.IsNotNull(() => _sut.GetAllAsync());
        }

        [TestMethod()]
        public void GetByIdAsyncTest()
        {
            var test = new FinancialCategory()
            {
                Type = FinanceType.Income,
                CategoryId = 2,
                Name = "Test_2"
            };

            _refitMock.Setup(x => x!.GetCategoryById(It.IsAny<int>()))
                .Returns(Task.FromResult(test));

            var result = _sut.GetByIdAsync(1);

           Assert.AreEqual(result.Result.Name, test.Name);
        }

        [TestMethod()]
        public void GetByIdAsyncInvalidDataTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentException>(() => _sut.GetByIdAsync(0));
        }
        [TestMethod()]
        public void UpdateAsyncInvalidDataTest()
        {
            Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _sut.UpdateAsync(null!));
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
