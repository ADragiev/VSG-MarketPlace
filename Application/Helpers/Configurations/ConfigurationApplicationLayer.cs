using Application.Helpers.Profiles;
using Application.Models.CategoryModels.Contacts;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Helpers.Configurations
{
    public static class ConfigurationApplicationLayer
    {
        public static IServiceCollection AddConfigurationApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CategoryProfile).Assembly);
            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
