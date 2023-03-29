using Application.Helpers.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Helpers.Configurations
{
    public static class ConfigurationApplicationLayer
    {
        public static IServiceCollection AddConfigurationApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile).Assembly);

            return services;
        }
    }
}
