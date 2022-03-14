using SocialRofl.Data;
using SocialRofl.Extensions;
using SocialRofl.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureSwagger();

builder.Services.AddDbContext<DataContext>();

builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.ConfigureIdentity(builder.Configuration);

builder.Services.AddLogic();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
