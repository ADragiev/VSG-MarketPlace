using Application.Helpers.Profiles;
using Application.Helpers.Validators;
using Application.Models.Cache;
using Application.Models.CategoryModels.Contacts;
using Application.Models.Cloud;
using Application.Models.EmailModels.Interfaces;
using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interfaces;
using Application.Models.LocationModels.Interfaces;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<ICloudService, CloudinaryService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IValidator<OrderCreateDto>, OrderCreateValidator>();
            services.AddScoped<IValidator<ProductCreateDto>, ProductCreateValidator>();
            services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateValidator>();
            services.AddScoped<IValidator<ImageCreateDto>, ImageCreateValidator>();
            return services;
        }
    }
}
