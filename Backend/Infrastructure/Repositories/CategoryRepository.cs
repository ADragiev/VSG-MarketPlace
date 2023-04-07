using Application.Models.GenericRepo;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(IMarketPlaceContext marketPlaceContext)
            : base(marketPlaceContext)
        {
        }
    }
}
