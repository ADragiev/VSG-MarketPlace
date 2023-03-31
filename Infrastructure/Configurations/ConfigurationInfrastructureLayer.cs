using Application.Models.GenericRepo;
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

            return services;
        }
    }
}
