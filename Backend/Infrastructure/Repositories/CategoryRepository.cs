using Application.Models.CategoryModels.Contacts;
using Application.Models.GenericRepo;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(IMarketPlaceContext marketPlaceContext)
            : base(marketPlaceContext)
        {
        }
    }
}
