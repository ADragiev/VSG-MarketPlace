using Application.Models.LocationModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.LocationModels.Interfaces
{
    public interface ILocationService
    {
        Task<List<LocationGetDto>> All();
    }
}
