namespace Befer.Server.Infrastructure.Extensions
{
    using Befer.Server.Data.Models;
    using Befer.Server.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using Befer.Server.Features.Posts;
    using Befer.Server.Features.Identity;
    using Microsoft.OpenApi.Models;
    using Befer.Server.Infrastructure.Filters;
    using Befer.Server.Features.Likes;
    using Befer.Server.Features.Comments;
    using Befer.Server.Features.GameScores;

    public static class ServiceCollectionExtensions
    {
        public static AppSettings GetAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsConfig = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsConfig);
            return appSettingsConfig.Get<AppSettings>();
        }

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<BeferDbContext>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddAppServices(this IServiceCollection services)
            => services
                .AddTransient<IPostService, PostService>()
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<ILikeService, LikeService>()
                .AddTransient<ICommentService, CommentService>()
                .AddTransient<IGameScoreService, GameScoreService>();

        public static IServiceCollection AddSwagger(this IServiceCollection services)
            => services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Befer API",
                        Version = "v1"
                    });
            });

        public static void AddApiControllers(this IServiceCollection services)
            => services
                .AddControllers(options => options.Filters.Add<ModelOrNotFoundActionFilter>());
    }
}
