using Application.Helpers.Profiles;
using Application.Helpers.Validators;
using Application.Models.CategoryModels.Contacts;
using Application.Models.Cloud;
using Application.Models.EmailModels.Interfaces;
using Application.Models.ImageModels.Dtos;
using Application.Models.ImageModels.Interfaces;
using Application.Models.LentItemModels.Dtos;
using Application.Models.LentItemModels.Interfaces;
using Application.Models.LocationModels.Interfaces;
using Application.Models.OrderModels.Dtos;
using Application.Models.OrderModels.Interfaces;
using Application.Models.ProductModels.Dtos;
using Application.Models.ProductModels.Intefaces;
using Application.Models.UserModels.Interfaces;
using Application.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Application.Helpers.Configurations
{
    public static class ConfigurationApplicationLayer
    {
        public static IServiceCollection AddConfigurationApplicationLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(typeof(CategoryProfile).Assembly);
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<ILentItemService, LentItemModelsService>();

            if (config["Redis:Enabled"] == "true")
            {
                services.AddStackExchangeRedisCache(options => options.ConfigurationOptions = new ConfigurationOptions
                {
                    EndPoints = { config["Redis:Connection"] },
                    Ssl = false,
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }

            services.AddScoped<ICloudService, CloudinaryService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, HttpUserService>();

            services.AddScoped<IValidator<OrderCreateDto>, OrderCreateValidator>();
            services.AddScoped<IValidator<ProductCreateDto>, ProductCreateValidator>();
            services.AddScoped<IValidator<ProductUpdateDto>, ProductUpdateValidator>();
            services.AddScoped<IValidator<ImageCreateDto>, ImageCreateValidator>();
            services.AddScoped<IValidator<LentItemCreateDto>, LentItemCreateValidator>();
            return services;
        }
    }
}
