using Befer.Server.Data;
using Befer.Server.Infrastructure.Extensions;
using Befer.Server.Infrastructure.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

services
    .AddDbContext<BeferDbContext>(options => options
        .UseSqlServer(builder.Configuration.GetDefaultConnectionString()))
    .AddIdentity()
    .AddJwtAuthentication(services.GetAppSettings(builder.Configuration))
    .AddAppServices()
    .AddSwagger()
    .AddApiControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app
        .UseDeveloperExceptionPage()
        .UseSwaggerUI();
}

app
    .UseRouting()
    .UseHttpsRedirection()
    .UseCors(options => options
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod())
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    })
    .ApplyMigrations();

app.Run();
