using CloudHub.API.Filters;
using CloudHub.API.Utils;
using CloudHub.Domain.Models;
using CloudHub.Domain.Services;
using CloudHub.Factories;
using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;

const string APP_SETTINGS_FILE = "appsettings.json";

/*
 * Configure App Builder
 */
var builder = WebApplication.CreateBuilder(args);
APIConfigurations settings = APIConfigurations.Load(APP_SETTINGS_FILE);
builder.Services.AddScoped<IGoogleServicesConfigurations>(_ => settings);
builder.Services.AddScoped<IEnvironmentSettings>(_ => settings);
//OAuth
builder.Services.AddScoped<GoogleOAuthExtractor>();
builder.Services.AddScoped<IOAuthService, OAuthService>();
//Databases
ApplyPendingMigrations(settings.MainConnectionString);
builder.Services.AddDbContext<DbContext, PostgreContext>(options => options.UseNpgsql(settings.MainConnectionString));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//Services
builder.Services.AddScoped<PingService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<NonceService>();
builder.Services.AddScoped<FeatureService>();
builder.Services.AddScoped<PurchaseService>();
//Filters
builder.Services.AddControllers(options => options.Filters.Add<ConsumerCredentialsFilter>());
//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
 * Build App
 */
var app = builder.Build();
if (settings.IsProductionModeEnabled == false) { app.UseSwagger(); app.UseSwaggerUI(); }
app.UseMiddleware<ErrorMiddleware>();
app.MapControllers();
app.Run();


static void ApplyPendingMigrations(string connectionString)
{
    DbContextOptionsBuilder dbBuilder = new();
    dbBuilder.UseNpgsql(connectionString);
    PostgreContext context = new(dbBuilder.Options);
    if (context.Database.GetPendingMigrations().ToList().Count > 0) { context.Database.Migrate(); }
}
