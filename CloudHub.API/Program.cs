using CloudHub.API;
using CloudHub.API.Filters;
using CloudHub.API.Middlewares;
using CloudHub.Domain.Repositories;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using CloudHub.Infra.Services;
using Microsoft.EntityFrameworkCore;

static string GetEnvVar(string var)
{
    return Environment.GetEnvironmentVariable(var) ?? throw new MissingEnvironmentVariableException(var);
}

string buildId = GetEnvVar("BUILD_ID");
string envName = GetEnvVar("ASPNETCORE_ENVIRONMENT");
string connectionString = GetEnvVar("API_DATABASE");
bool isProduction = bool.Parse(GetEnvVar("PRODUCTION_MODE"));
string googleTokenInfoApiUrl = GetEnvVar("GOOGLE_TOKEN_INFO_API_URL");
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
