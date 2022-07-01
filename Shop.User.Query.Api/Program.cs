using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;
using Shop.Infrastructure;
using Shop.Infrastructure.Extensions;
using Shop.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.

builder.Services.AddControllers();
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
services.Configure<IdentityOptions>(config =>
{
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
});
services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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