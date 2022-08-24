namespace Befer.Server.Infrastructure
{
    using Befer.Server.Data;
    using Microsoft.EntityFrameworkCore;

    public static class ApplicationBuilderExtentions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<BeferDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
