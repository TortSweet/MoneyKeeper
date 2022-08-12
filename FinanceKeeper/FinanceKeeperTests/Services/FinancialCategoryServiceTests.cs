using AutoMapper;
using FinanceKeeper.AutoMapperProfiles;
using FinanceKeeper.Data.Entities;
using FinanceKeeper.Dtos;
using FinanceKeeper.Repositories.Abstracions;
using FinanceKeeper.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;



namespace FinanceKeeperTests.Services
{
    [TestClass()]
    public class FinancialCategoryServiceTests
    {
        private readonly FinancialCategoryService _sut;
        private readonly Mock<IFinancialCategoryRepository?> _repositoryMock = new();
        private static MapperConfiguration config = new(x => x
            .AddProfile(new FinancialCategoryProfile()));
        private IMapper? _mapper = new Mapper(config);

        public FinancialCategoryServiceTests()
        {
            _sut = new FinancialCategoryService(_repositoryMock.Object, _mapper);
        }
        private readonly FinancialCategory? category = new FinancialCategory()
        {
            Type = FinanceType.Expense,
            CategoryId = 1,
            Name = "Test"
        };
        private readonly FinancialCategoryDto? categoryDto = new FinancialCategoryDto()
        {
            Type = FinanceType.Expense,
            CategoryId = 1,
            Name = "Test"
        };


        [TestMethod()]
        public void CreateEntryNullConstructorAsyncTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new FinancialCategoryService(null, null));
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
                => x!.CreateCategoryAsync(It.IsAny<FinancialCategory>())).Returns(Task.FromResult(category));

            var result = _sut.CreateEntryAsync(categoryDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(categoryDto?.CategoryId, result.Result.CategoryId);
            Assert.AreEqual(categoryDto?.Type, result.Result.Type);
            Assert.AreEqual(categoryDto?.Name, result.Result.Name);
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
                => x!.UpdateCategoryAsync(It.IsAny<FinancialCategory>())).Returns(Task.FromResult(category));

            var result = _sut.UpdateEntryAsync(categoryDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(categoryDto?.CategoryId, result.Result.CategoryId);
            Assert.AreEqual(categoryDto?.Type, result.Result.Type);
            Assert.AreEqual(categoryDto?.Name, result.Result.Name);
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
                x!.DeleteCategoryAsync(It.IsAny<int>())).Returns(Task.FromResult(true));
            
            var resultTrue = _sut.DeleteEntryAsync(1);

            Assert.AreEqual(resultTrue.Result, true);

            _repositoryMock.Setup(x => 
                x!.DeleteCategoryAsync(It.IsAny<int>())).Returns(Task.FromResult(false));

            var resultFalse = _sut.DeleteEntryAsync(1);

            Assert.AreEqual(resultFalse.Result, false);
        }
        [TestMethod()]
        public void GetAllEntriesTest()
        {
            var listCategories = new List<FinancialCategory?>()
            {
                category,
                new FinancialCategory()
                {
                    Type = FinanceType.Expense,
                    CategoryId = 2,
                    Name = "Test 2"
                }
            };
            
            _repositoryMock.Setup(x =>
                x!.GetAllCategories()).Returns(listCategories.AsQueryable);

            var result = _sut.GetAllEntries();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Test 2", result[1].Name);
        }
        [TestMethod()]
        public void GetEntryByIdNullTest()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _sut.GetEntryById(0));
        }
        [TestMethod()]
        public void GetEntryByIdTest()
        {
            _repositoryMock.Setup(x => x!.GetCategoryById(It.IsAny<int>())).Returns(category);
            var result = _sut.GetEntryById(1);

            Assert.AreEqual(categoryDto?.CategoryId, result.CategoryId);
            Assert.AreEqual(categoryDto?.Type, result.Type);
            Assert.AreEqual(categoryDto?.Name, result.Name);
        }
    }
}
