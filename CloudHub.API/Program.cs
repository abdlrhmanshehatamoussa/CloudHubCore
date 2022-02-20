using CloudHub.API;
using CloudHub.API.Filters;
using CloudHub.API.Startup;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Configurations configurations = Configurations.Load("appsettings.json");
builder.Services.AddSingleton<Configurations>(configurations);

builder.Services.AddControllers(options => options.Filters.Add<ConsumerCredentialsFilter>());
builder.RegisterDomainServices(configurations);
builder.ConfigureSwaggerServices(configurations);
Migrations.ApplyPendingMigrations(configurations.MainConnectionString);


WebApplication app = builder.Build();
app.ConfigureSwagger(configurations);
app.UseMiddleware<ErrorMiddleware>();
app.MapControllers();
app.Run();