using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shop.Domain.Commands;
using Shop.Domain.Events;
using Shop.Domain.Queries;
using Shop.Infrastructure.MessageBus;
using Shop.Infrastructure.Mongo;

namespace Shop.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<MongoConfig>()
            .Bind(configuration)
            .ValidateDataAnnotations();

        services.AddSingleton<IMongoClient>(provider =>
        {
            var connectionString = provider.GetRequiredService<IOptions<MongoConfig>>().Value.ConnectionString;
            
            return new MongoClient(connectionString);
        });
        
        services.AddSingleton<IMongoDatabase>(provider =>
        {
            var mongoClient = provider.GetRequiredService<IMongoClient>();
            var database = provider.GetRequiredService<IOptions<MongoConfig>>().Value.Database;

            return mongoClient.GetDatabase(database);
        });
    }

    public static void AddRabbitMq(this IServiceCollection services, 
        IConfiguration configuration,
        Assembly assemblySource)
    {
        services.AddOptions<RabbitMqConfig>()
            .Bind(configuration)
            .ValidateDataAnnotations();

        services.AddMassTransit(config =>
        {
            config.AddConsumers(assemblySource);
            config.AddBus(context =>Bus.Factory.CreateUsingRabbitMq(config =>
            {
                var rabbitConfig = context.GetRequiredService<IOptions<RabbitMqConfig>>().Value;

                config.Host(new Uri(rabbitConfig.ConnectionString), hconfig =>
                {
                    hconfig.Username(rabbitConfig.Username);
                    hconfig.Password(rabbitConfig.Password);
                });
                
                config.ConfigureEndpoints(context);
            }));

            
        });
    }
}