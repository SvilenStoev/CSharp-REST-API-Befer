using Befer.Server.Infrastructure.Extensions;

namespace Befer.Server.Infrastructure.Extensions
{
    using Befer.Server.Data;
    using Microsoft.EntityFrameworkCore;

    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
         => app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Befer API");
                options.RoutePrefix = string.Empty;
            });

        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<BeferDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
