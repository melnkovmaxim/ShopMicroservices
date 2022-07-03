using System.Reflection;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Shop.Cart.DataProvider.Repositories;
using Shop.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddRabbitMq(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddStackExchangeRedisCache(cfg =>
{
    cfg.Configuration = $"{builder.Configuration["REDIS:REDIS_HOST"]}:{builder.Configuration["REDIS:REDIS_PORT"]}";
});
builder.Services.AddScoped<ICartRepository, CartRepository>();
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