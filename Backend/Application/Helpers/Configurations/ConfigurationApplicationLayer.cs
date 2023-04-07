using Application.Helpers.Profiles;
using Application.Models.CategoryModels.Contacts;
using Application.Models.ImageModels.Interfaces;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Intefaces;
using Application.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Helpers.Configurations
{
    public static class ConfigurationApplicationLayer
    {
        public static IServiceCollection AddConfigurationApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CategoryProfile).Assembly);
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IImageService, ImageService>();

            services.AddControllers()
                .AddFluentValidation(c => c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
