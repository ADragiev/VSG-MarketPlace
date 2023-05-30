using Application.Models.LocationModels.Dtos;
using Application.Models.LocationModels.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using System.Text;
using System.Text.Json;

namespace Tests
{
    public class LocationServiceTests
    {
        private Mock<ILocationRepository> locationRepoMock;
        private Mock<IMapper> mapperMock;
        private Mock<IDistributedCache> cacheServiceMock;

        private ILocationService locationService;

        [SetUp]
        public void SetUp()
        {
            locationRepoMock = new Mock<ILocationRepository>();
            mapperMock = new Mock<IMapper>();
            cacheServiceMock = new Mock<IDistributedCache>();

            locationService = new LocationService(locationRepoMock.Object, mapperMock.Object, cacheServiceMock.Object);
        }

        [Test]
        public async Task AllAsync_ShouldReturnCachedLocations_IfTheyAreCached()
        {
            cacheServiceMock.Setup(c => c.GetAsync("locations-angel", default)).ReturnsAsync(() => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new List<LocationGetDto>() {
                new LocationGetDto()
                {
                    Id = 1,
                    Name = "Test"
                }
            })));
            var locations = await locationService.AllAsync();
            Assert.That(locations[0].Id, Is.EqualTo(1));
            Assert.That(locations[0].Name, Is.EqualTo("Test"));
        }

        [Test]
        public async Task AllAsync_ShouldReturnLocationsFromDatabase_IfTheyAreNotCached()
        {
            cacheServiceMock.Setup(c => c.GetAsync("locations-angel", default)).ReturnsAsync(() => null);
            locationRepoMock.Setup(l => l.AllAsync()).ReturnsAsync(() => new List<Location>() {
                new Location()
                {
                    Id = 1,
                    Name = "Test"
                }
            });

            mapperMock.Setup(m => m.Map<List<LocationGetDto>>(It.IsAny<List<Location>>())).Returns(() => new List<LocationGetDto>() {
                new LocationGetDto()
                {
                    Id = 1,
                    Name = "Test"
                }
            });

            var locations = await locationService.AllAsync();
            Assert.That(locations[0].Id, Is.EqualTo(1));
            Assert.That(locations[0].Name, Is.EqualTo("Test"));
        }
    }
}
