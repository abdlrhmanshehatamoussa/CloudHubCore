using CloudHub.API;
using CloudHub.API.Filters;
using CloudHub.API.Middlewares;
using CloudHub.Domain.Repositories;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using CloudHub.Infra.Services;
using Microsoft.EntityFrameworkCore;

static string GetEnvVar(string var, string? defaultValue = null)
{
    string? value = Environment.GetEnvironmentVariable(var);
    if (value != null) { return value; }
    if (defaultValue != null) { return defaultValue; }
    throw new MissingEnvironmentVariableException(var);
}

bool isProduction = bool.Parse(GetEnvVar("PRODUCTION_MODE"));
string buildId = GetEnvVar("BUILD_ID", "0.0.0");
string envName = GetEnvVar("ASPNETCORE_ENVIRONMENT", "Local");
string connectionString = GetEnvVar("API_DATABASE", isProduction ? null : "Host=127.0.0.1;Database=cloudhub-api-core-local;Username=postgres;Password=123456");
string googleTokenInfoApiUrl = GetEnvVar("GOOGLE_TOKEN_INFO_API_URL", "");
APIConfigurations settings = new(envName, buildId, isProduction, connectionString, googleTokenInfoApiUrl);


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<APIConfigurations>((_) => settings);
builder.Services.AddScoped<IGoogleServicesConfigurations>((_) => settings);
builder.Services.AddScoped<GoogleOAuthExtractor>();
builder.Services.AddScoped<IOAuthService, OAuthService>();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IServiceConfigurations>((_) => settings);
builder.Services.AddScoped<BaseService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<NonceService>();
builder.Services.AddScoped<FeatureService>();
builder.Services.AddScoped<PurchaseService>();
builder.Services.AddScoped<PublicDataService>();
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
