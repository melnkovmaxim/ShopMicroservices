using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Shop.Infrastructure.Authentication;
using Shop.Infrastructure.Extensions;
using Shop.Infrastructure.Mongo;
using Shop.Infrastructure.Options;
using Shop.User.Api;
using Shop.User.Api.Entities;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.

services.AddOptions<SqlOptions>()
    .Bind(configuration)
    .ValidateDataAnnotations();

services.AddOptions<JwtOptions>()
    .Bind(configuration)
    .ValidateDataAnnotations();

services.AddControllers();
services.AddJwtAuthentication(configuration);
services.AddRabbitMq(configuration, Assembly.GetExecutingAssembly());
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration["SQL:CONNECTION_STRING"]));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.Configure<IdentityOptions>(config =>
{
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
});
services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var sqlDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    sqlDb.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();