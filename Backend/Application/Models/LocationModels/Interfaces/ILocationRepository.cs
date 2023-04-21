using Application.Models.GenericRepo;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LocationModels.Interfaces
{
    public interface ILocationRepository : IGenericRepository<Location>
    {
    }
}
