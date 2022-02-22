namespace CloudHub.API.Startup
{
    internal static partial class ApplicationBuilderExtensions
    {
        internal static void ConfigureSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
