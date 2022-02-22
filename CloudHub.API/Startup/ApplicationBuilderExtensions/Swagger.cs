namespace CloudHub.API.Startup
{
    public static partial class ApplicationBuilderExtensions
    {
        public static void ConfigureSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
