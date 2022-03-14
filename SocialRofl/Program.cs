using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialRofl.Data;
using SocialRofl.Interfaces;
using SocialRofl.Models.Database;
using System.Text;
using Logic = SocialRofl.Logic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
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
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddDbContext<DataContext>();
builder.Services.AddIdentityCore<User>(options => options.Password = new PasswordOptions
{
    RequireDigit = false,
    RequiredLength = 1,
    RequiredUniqueChars = 0,
    RequireLowercase = false,
    RequireNonAlphanumeric = false,
    RequireUppercase = false
})
    .AddEntityFrameworkStores<DataContext>();
var mySecret = builder.Configuration.GetValue<string>("SecurityKey");
var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", options =>
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

builder.Services.AddScoped<Logic.AttachmentChecker>();
builder.Services.AddScoped<Logic.Auth>();
builder.Services.AddScoped<Logic.PhotoLogic>();
builder.Services.AddScoped<Logic.FriendsLogic>();
builder.Services.AddScoped<Logic.SearchLogic>();
builder.Services.AddScoped<Logic.UserLogic>();
builder.Services.AddScoped<Logic.WallLogic>();
builder.Services.AddScoped<IHashGenerator, Logic.HashGenerator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler("/error");

app.MapControllers();

app.Run();
