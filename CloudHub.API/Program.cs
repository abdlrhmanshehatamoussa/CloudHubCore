using CloudHub.API;
using CloudHub.API.Filters;
using CloudHub.API.Startup;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Configurations configurations = ConfigurationsLoader.Load("appsettings.json");
builder.Services.AddSingleton<Configurations>(configurations);

builder.RegisterDbContext(configurations);
builder.RegisterFactories(configurations);
builder.RegisterDomainServices(configurations);
builder.Services.AddControllers(options => options.Filters.Add<ConsumerCredentialsFilter>());
builder.ConfigureSwaggerServices(configurations);


WebApplication app = builder.Build();
app.ConfigureSwagger(configurations);
app.UseMiddleware<ErrorMiddleware>();
app.MapControllers();

Migrations.ApplyPendingMigrations(configurations.MainConnectionString);
app.Run();