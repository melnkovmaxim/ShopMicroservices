using System.Reflection;
using MassTransit;
using MongoDB.Driver;
using Shop.Domain.Commands;
using Shop.Infrastructure;
using Shop.Infrastructure.Extensions;
using Shop.Infrastructure.Mongo;
using Shop.Product.DataProvider.Repositories;
using Shop.Product.DataProvider.Services;
using Shop.Product.Query.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
// Add services to the container.

services.AddControllers();
services.AddMongoDb(configuration);
services.AddRabbitMq(configuration, Assembly.GetExecutingAssembly());
services.AddJwtAuthentication(configuration);
services.AddScoped<IProductRepository, ProductRepository>();
services.AddScoped<IProductService, ProductService>();
services.AddHostedService<BusWorker>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mongoDb = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();

    MongoInitializer.Initialize(mongoDb);
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