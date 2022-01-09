using CloudHub.API.Common;
using CloudHub.API.Filters;
using CloudHub.API.Middlewares;
using CloudHub.Domain.Repositories;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using CloudHub.Infra.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

/*
 * Configure App Builder
 */
APIConfigurations settings = APIConfigurations.Load();
builder.Services.AddScoped<IGoogleServicesConfigurations>(_ => settings);
builder.Services.AddScoped<IEnvironmentSettings>(_ => settings);
//OAuth
builder.Services.AddScoped<GoogleOAuthExtractor>();
builder.Services.AddScoped<IOAuthService, OAuthService>();
//Databases
builder.Services.AddDbContext<PostgreDatabase>(options => { options.UseNpgsql(settings.ConnectionString); });
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
