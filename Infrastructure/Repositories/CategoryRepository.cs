using Domain.Entities;
using Infrastructure.Repositories.GenericRepository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(MarketPlaceContext marketPlaceContext)
            : base(marketPlaceContext)
        {
        }
    }
}
