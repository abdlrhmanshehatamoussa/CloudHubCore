using CloudHub.API.Commons;
using CloudHub.API.Filters;
using CloudHub.API.Middlewares;
using CloudHub.Domain.Entities;
using CloudHub.Domain.Repositories;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using CloudHub.Infra.Services;
using Microsoft.EntityFrameworkCore;

const string APP_SETTINGS_FILE = "appsettings.json";
const string TENANTS_FILE = "tenants.json";

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
List<Tenant> tenants = TenantsService.LoadTenants(TENANTS_FILE);
builder.Services.AddScoped<ITenantsService>(_ => new TenantsService(tenants));
builder.Services.AddDbContext<DbContext, PostgreContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//Services
builder.Services.AddScoped<BaseService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<NonceService>();
builder.Services.AddScoped<FeatureService>();
builder.Services.AddScoped<PurchaseService>();
builder.Services.AddScoped<PublicDataService>();
builder.Services.AddScoped<PrivateDataService>();
//Filters
builder.Services.AddControllers(options => options.Filters.Add<ConsumerCredentialsFilter>());
builder.Services.AddControllers(options => options.Filters.Add<TenantFilter>());
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