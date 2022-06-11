using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shop.Infrastructure.Mongo;

namespace Shop.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMongoDb(this IServiceCollection services)
    {
        services.AddOptions<MongoConfig>()
            .BindConfiguration("MONGO")
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
}