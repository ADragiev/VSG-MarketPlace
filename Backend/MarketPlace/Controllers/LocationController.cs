using Application.Helpers.Constants;
using Application.Models.LocationModels.Dtos;
using Application.Models.LocationModels.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    public class LocationController : BaseController
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet]
        public async Task<List<LocationGetDto>> GetAll()
        {
            return await locationService.AllAsync();
        }
    }
}
