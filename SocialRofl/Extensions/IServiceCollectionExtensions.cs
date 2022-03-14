using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialRofl.Data;
using SocialRofl.Interfaces;
using SocialRofl.Models.Database;
using System.Text;

namespace SocialRofl.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection collection)
        {
            collection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SocialRofl", Version = "v1.0.0" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);

            });
        }
        
        public static void ConfigureIdentity(this IServiceCollection collection, IConfiguration config)
        {
            collection.AddIdentityCore<User>(options => options.Password = new PasswordOptions
            {
                RequireDigit = false,
                RequiredLength = 1,
                RequiredUniqueChars = 0,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequireUppercase = false
            }).AddEntityFrameworkStores<DataContext>();
            var mySecret = config.GetValue<string>("SecurityKey");
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
            collection.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = mySecurityKey
                };
            });
        }

        public static void AddLogic(this IServiceCollection collection)
        {
            collection.AddScoped<Logic.AttachmentChecker>();
            collection.AddScoped<Logic.Auth>();
            collection.AddScoped<Logic.PhotoLogic>();
            collection.AddScoped<Logic.FriendsLogic>();
            collection.AddScoped<Logic.SearchLogic>();
            collection.AddScoped<Logic.UserLogic>();
            collection.AddScoped<Logic.WallLogic>();
            collection.AddScoped<IHashGenerator, Logic.HashGenerator>();
        }
    }
}
