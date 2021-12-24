using CloudHub.API;
using CloudHub.API.Filters;
using CloudHub.API.Middlewares;
using CloudHub.Domain.Repositories;
using CloudHub.Domain.Services;
using CloudHub.Infra.Data;
using CloudHub.Infra.Services;
using Microsoft.EntityFrameworkCore;


var configurationBuilder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
IConfiguration configuration = configurationBuilder.Build();

string buildId = configuration.GetValue<string>("build_id");
string envName = configuration.GetValue<string>("env");
string connectionString = configuration.GetValue<string>("api_database");
bool isProduction = configuration.GetValue<bool>("production_mode");
string googleTokenInfoApiUrl = configuration.GetValue<string>("google_token_info_api_url");

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
