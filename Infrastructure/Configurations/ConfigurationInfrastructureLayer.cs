using Infrastructure.Repositories.GenericRepository.Context;
using Infrastructure.Repositories.GenericRepository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations
{
    public static class ConfigurationInfrastructureLayer
    {
        public static IServiceCollection AddConfigurationInfrastructureLayer(this IServiceCollection services)
        {
            services.AddSingleton<IContext, MarketPlaceContext>();

            return services;
        }
    }
}
