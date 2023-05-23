using Application.Helpers.Configurations;
using Application.Helpers.Constants;
using Application.Helpers.Middlewares;
using Infrastructure.Configurations;
using MarketPlace.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using NLog.Web;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
}).UseNLog();
IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile(path: "appsettings.json").Build();

NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = config;

builder.Services.AddConfigurationApiLayer(config);
builder.Services.AddConfigurationApplicationLayer();
builder.Services.AddConfigurationInfrastructureLayer();
builder.Services.AddConfigurationMigration();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
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
