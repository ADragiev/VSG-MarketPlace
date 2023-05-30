using Application.Models.CacheModels.Interfaces;
using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace Tests
{
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> categoryRepoMock;
        private Mock<IMapper> mapperMock;
        private Mock<ICacheService> cacheServiceMock;

        private ICategoryService categoryService;

        [SetUp]
        public void SetUp()
        {
            categoryRepoMock = new Mock<ICategoryRepository>();
            mapperMock = new Mock<IMapper>();
            cacheServiceMock = new Mock<ICacheService>();

            categoryService = new CategoryService(categoryRepoMock.Object, mapperMock.Object, cacheServiceMock.Object);
        }

        [Test]
        public async Task AllAsync_ShouldReturnCachedCategories_IfTheyAreCached()
        {
            cacheServiceMock.Setup(c => c.GetDataAsync<List<CategoryGetDto>>("categories-angel")).ReturnsAsync(() => new List<CategoryGetDto>()
            {
                new CategoryGetDto()
                {
                    Id= 1,
                    Name = "Test"
                }
            });

            var categories = await categoryService.AllAsync();

            Assert.That(categories[0].Id, Is.EqualTo(1));
            Assert.That(categories[0].Name, Is.EqualTo("Test"));
        }

        [Test]
        public async Task AllAsync_ShouldReturnCategoriesFromDatabase_IfTheyAreNotCached()
        {
            cacheServiceMock.Setup(c => c.GetDataAsync<List<CategoryGetDto>>("categories")).ReturnsAsync(() => null);

            categoryRepoMock.Setup(c => c.AllAsync()).ReturnsAsync(() => new List<Category>()
            {
                new Category()
                {
                    Id= 1,
                    Name = "Test"
                }
            });

            mapperMock.Setup(m=>m.Map<List<CategoryGetDto>>(It.IsAny<List<Category>>())).Returns(() => new List<CategoryGetDto>()
            {
                new CategoryGetDto()
                {
                    Id= 1,
                    Name = "Test"
                }
            });

            var categories = await categoryService.AllAsync();
            Assert.That(categories[0].Id, Is.EqualTo(1));
            Assert.That(categories[0].Name, Is.EqualTo("Test"));
        }
    }
}
