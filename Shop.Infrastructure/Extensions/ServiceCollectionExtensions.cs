using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
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

    public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<RabbitMqConfig>()
            .Bind(configuration)
            .ValidateDataAnnotations();
        
        services.AddMassTransit(config =>
        {
            config.AddBus(context => context.AddRabbitMqBus());
        });
    }

    public static void AddRabbitMq(this IServiceCollection services, 
        IConfiguration configuration,
        string endpoint,
        IEnumerable<Action<IBusRegistrationConfigurator>> consumerRegistrations,
        IEnumerable<Action<IReceiveEndpointConfigurator, IBusRegistrationContext>> consumerRetryRegistrations)
    {
        services.AddOptions<RabbitMqConfig>()
            .Bind(configuration)
            .ValidateDataAnnotations();
        
        services.AddMassTransit(config =>
        {
            foreach (var consumer in consumerRegistrations)
            {
                consumer.Invoke(config);
            }
            
            config.AddBus(context => context.AddRabbitMqBus(endpoint, consumerRetryRegistrations));
        });
    }
}