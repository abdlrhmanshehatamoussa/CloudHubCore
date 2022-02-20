using CloudHub.API;
using CloudHub.API.Filters;
using CloudHub.API.Startup;
using CloudHub.Infra.Data;
using Microsoft.EntityFrameworkCore;


Configurations configurations = Configurations.Load("appsettings.json");

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options => options.Filters.Add<ConsumerCredentialsFilter>());
builder.RegisterDomainServices(configurations);
builder.ConfigureSwaggerServices(configurations);
ApplyPendingMigrations(configurations.MainConnectionString);

WebApplication app = builder.Build();
app.ConfigureSwagger(configurations);
app.UseMiddleware<ErrorMiddleware>();
app.MapControllers();
app.Run();


static void ApplyPendingMigrations(string connectionString)
{
    DbContextOptionsBuilder dbBuilder = new();
    dbBuilder.UseNpgsql(connectionString);
    PostgreContext context = new(dbBuilder.Options);
    if (context.Database.GetPendingMigrations().ToList().Count > 0) { context.Database.Migrate(); }
}
