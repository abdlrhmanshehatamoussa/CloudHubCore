using CloudHub.API;
using CloudHub.API.Filters;
using CloudHub.API.Middlewares;
using CloudHub.Domain.Repositories;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using CloudHub.Infra.Services;
using Microsoft.EntityFrameworkCore;


//TODO: Load App settings from configurations instead of hard-coded
string buildId = "000";
string envName = "Development";
string connectionString = "Host=127.0.0.1;Database=cloudhub-api2;Username=postgres;Password=123456";
bool isProduction = false;
APISettings settings = new APISettings(envName, buildId, isProduction, connectionString);


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<APISettings>((_) => settings);
builder.Services.AddScoped<IOAuthService,OAuthService>();
builder.Services.AddDbContext<MyDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductionModeProvider>((_) => settings);
builder.Services.AddScoped<BaseService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<NonceService>();
builder.Services.AddScoped<ReleaseService>();
builder.Services.AddScoped<FeatureService>();
builder.Services.AddScoped<UserActionService>();
builder.Services.AddScoped<PurchaseService>();
builder.Services.AddControllers(options => options.Filters.Add<ConsumerCredentialsFilter>());

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (isProduction == false)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorMiddleware>();
app.MapControllers();

app.Run();
