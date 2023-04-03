using Application.Models.GenericRepo;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Intefaces;
using Infrastructure.Repositories;
using Infrastructure.Repositories.GenericRepository.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations
{
    public static class ConfigurationInfrastructureLayer
    {
        public static IServiceCollection AddConfigurationInfrastructureLayer(this IServiceCollection services)
        {
            services.AddSingleton<MarketPlaceContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
