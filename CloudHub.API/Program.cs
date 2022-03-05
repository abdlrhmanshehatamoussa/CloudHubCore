using CloudHub.API;
using CloudHub.API.Filters;
using CloudHub.API.Startup;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
CloudHubApiConfigurations configurations = CloudHubApiConfigurations.Load(builder.Configuration);

builder.InjectConfigurations(configurations);
builder.RegisterServiceProviders();
builder.RegisterDomainServices();
builder.Services.AddControllers(options => options.Filters.Add<ConsumerCredentialsFilter>());
if (!configurations.IsProduction) builder.ConfigureSwaggerServices();

WebApplication app = builder.Build();
if (!configurations.IsProduction) app.ConfigureSwagger();
app.UseMiddleware<ErrorMiddleware>();
app.MapControllers();

app.Run();

#pragma warning disable CA1050 // Declare types in namespaces
public partial class Program { }
#pragma warning restore CA1050 // Declare types in namespaces