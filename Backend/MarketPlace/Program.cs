using Application.Helpers.Configurations;
using Application.Helpers.Middlewares;
using Infrastructure.Configurations;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
}).UseNLog();

IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile(path: "appsettings.json").Build();
NLog.Extensions.Logging.ConfigSettingLayoutRenderer.DefaultConfiguration = config;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddConfigurationApplicationLayer();
builder.Services.AddConfigurationInfrastructureLayer();
builder.Services.AddConfigurationMigration();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
