using Application.Helpers.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MarketPlace.Configurations
{
    public static class ConfigurationApiLayer
    {
        public static IServiceCollection AddConfigurationApiLayer(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigurationSwaggerOptions>();
            services.AddEndpointsApiExplorer();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Authority = config["AzureSettings:Authority"];
                    options.Audience = config["AzureSettings:Client"];
                });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CORSPolicy", policy =>
                {
                    policy
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin();
                });
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(IdentityConstants.AdminRolePolicyName, p =>
                {
                    p.RequireClaim(IdentityConstants.AdminRoleClaimName, "true");
                });
            });

            return services;
        }
    }
}
