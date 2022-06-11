using MassTransit;
using MongoDB.Driver;
using Shop.Infrastructure;
using Shop.Infrastructure.Extensions;
using Shop.Infrastructure.Mongo;
using Shop.Product.Api;
using Shop.Product.Api.Handlers;
using Shop.Product.Api.Repositories;
using Shop.Product.Api.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var env = builder.Environment;
var configuration = builder.Configuration;

// Add services to the container.

configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
    .AddEnvironmentVariables();

services.AddControllers();
services.AddMongoDb(configuration);
services.AddRabbitMq(configuration, "create_product",
new List<Action<IBusRegistrationConfigurator>>()
{
    c => c.AddConsumer<ProductCreateHandler>()
},
new List<Action<IReceiveEndpointConfigurator, IBusRegistrationContext>>()
{
    (cfg, ctx) => cfg.ConfigureConsumer<ProductCreateHandler>(ctx)
});
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