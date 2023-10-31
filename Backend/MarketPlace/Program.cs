using Application.Helpers.Configurations;
using Application.Helpers.Middlewares;
using Infrastructure.Configurations;
using MarketPlace.Configurations;
using NLog.Web;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
}).UseNLog();
IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile(path: "appsettings.json").Build();

NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = config;
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
builder.Services.AddConfigurationApiLayer(config);
builder.Services.AddConfigurationApplicationLayer(config);
builder.Services.AddConfigurationInfrastructureLayer();
builder.Services.AddConfigurationMigration();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CORSPolicy");
app.CreateDatabase();
app.MigrateDatabase();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
