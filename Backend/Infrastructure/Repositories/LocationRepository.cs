using Application.Models.GenericRepo;
using Application.Models.LocationModels.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        public LocationRepository(IMarketPlaceContext marketPlaceContext)
            : base(marketPlaceContext)
        {
        }
    }
}
