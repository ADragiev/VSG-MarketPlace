using Application.Models.CategoryModels.Contacts;
using Application.Models.CategoryModels.Dtos;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Text;
using System.Text.Json;

namespace Tests
{
    public class CategoryServiceTests
    {
        private Mock<ICategoryRepository> categoryRepoMock;
        private Mock<IMapper> mapperMock;
        private Mock<IDistributedCache> cacheServiceMock;

        private ICategoryService categoryService;

        [SetUp]
        public void SetUp()
        {
            categoryRepoMock = new Mock<ICategoryRepository>();
            mapperMock = new Mock<IMapper>();
            cacheServiceMock = new Mock<IDistributedCache>();
            categoryService = new CategoryService(categoryRepoMock.Object, mapperMock.Object, cacheServiceMock.Object);
        }

        [Test]
        public async Task AllAsync_ShouldReturnCachedCategories_IfTheyAreCached()
        {
            var cacheReturn = 

            cacheServiceMock.Setup(c => c.GetAsync("categories-angel", default)).ReturnsAsync(() => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new List<CategoryGetDto>()
            {
                new CategoryGetDto()
                {
                    Id= 1,
                    Name = "Test"
                }
            })));

            var categories = await categoryService.AllAsync();

            Assert.That(categories[0].Id, Is.EqualTo(1));
            Assert.That(categories[0].Name, Is.EqualTo("Test"));
        }

        [Test]
        public async Task AllAsync_ShouldReturnCategoriesFromDatabase_IfTheyAreNotCached()
        {
            cacheServiceMock.Setup(c => c.GetAsync("categories-angel", default)).ReturnsAsync(() => null);

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
