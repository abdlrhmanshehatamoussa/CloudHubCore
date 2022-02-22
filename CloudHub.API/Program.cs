using CloudHub.API;
using CloudHub.API.Filters;
using CloudHub.API.Startup;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

CloudHubApiConfigurations configurations = builder.InjectConfigurations();
builder.RegisterDbContext(configurations);
builder.RegisterServiceImplementations(configurations);
builder.RegisterDomainServices();
builder.Services.AddControllers(options => options.Filters.Add<ConsumerCredentialsFilter>());
builder.ConfigureSwaggerServices(configurations);

WebApplication app = builder.Build();
app.ConfigureSwagger(configurations);
app.UseMiddleware<ErrorMiddleware>();
app.MapControllers();

app.Run();

public partial class Program { }