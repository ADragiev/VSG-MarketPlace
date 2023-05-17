using Application.Helpers.Configurations;
using Application.Helpers.Constants;
using Application.Helpers.Middlewares;
using Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
}).UseNLog();
IConfigurationRoot config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = config;

builder.Services.AddControllers();
builder.Services.AddConfigurationApplicationLayer();
builder.Services.AddConfigurationInfrastructureLayer();
builder.Services.AddConfigurationMigration();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigurationSwaggerOptions>();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.Authority = config["AzureSettings:Authority"];
        options.Audience = config["AzureSettings:Client"];
    });
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
 {
     options.AddPolicy(name: "CORSPolicy", policy =>
     {
         policy
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowAnyOrigin();
     });
 });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityConstants.AdminRolePolicyName, p =>
    {
        p.RequireClaim(IdentityConstants.AdminRoleClaimName, "true");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CORSPolicy");
app.CreateDatabase();
app.CreateTables();
app.CreateViewsAndFunctions();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
