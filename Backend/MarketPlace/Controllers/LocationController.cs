using Application.Models.LocationModels.Dtos;
using Application.Models.LocationModels.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet]
        public async Task<List<LocationGetDto>> GetAll()
        {
            return await locationService.All();
        }
    }
}
