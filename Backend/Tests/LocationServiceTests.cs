using Application.Models.Cache;
using Application.Models.LocationModels.Dtos;
using Application.Models.LocationModels.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class LocationServiceTests
    {
        private Mock<ILocationRepository> locationRepoMock;
        private Mock<IMapper> mapperMock;
        private Mock<ICacheService> cacheServiceMock;

        private ILocationService locationService;

        [SetUp]
        public void SetUp()
        {
            locationRepoMock = new Mock<ILocationRepository>();
            mapperMock = new Mock<IMapper>();
            cacheServiceMock = new Mock<ICacheService>();

            locationService = new LocationService(locationRepoMock.Object, mapperMock.Object, cacheServiceMock.Object);
        }

        [Test]
        public async Task AllAsync_ShouldReturnCachedLocations_IfTheyAreCached()
        {
            cacheServiceMock.Setup(c => c.GetData<List<LocationGetDto>>("locations")).ReturnsAsync(() => new List<LocationGetDto>() {
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

        [Test]
        public async Task AllAsync_ShouldReturnLocationsFromDatabase_IfTheyAreNotCached()
        {
            cacheServiceMock.Setup(c => c.GetData<List<LocationGetDto>>("locations")).ReturnsAsync(()=>null);
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
