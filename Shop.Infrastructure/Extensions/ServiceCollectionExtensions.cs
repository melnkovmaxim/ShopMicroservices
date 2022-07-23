using System.Reflection;
using System.Text;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using Shop.Domain.Commands;
using Shop.Domain.Events;
using Shop.Domain.Queries;
using Shop.Infrastructure.Authentication;
using Shop.Infrastructure.Mongo;
using Shop.Infrastructure.Options;

namespace Shop.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<MongoOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations();

        services.AddSingleton<IMongoClient>(provider =>
        {
            var connectionString = provider.GetRequiredService<IOptions<MongoOptions>>().Value.ConnectionString;

            return new MongoClient(connectionString);
        });

        services.AddSingleton<IMongoDatabase>(provider =>
        {
            var mongoClient = provider.GetRequiredService<IMongoClient>();
            var database = provider.GetRequiredService<IOptions<MongoOptions>>().Value.Database;

            return mongoClient.GetDatabase(database);
        });
    }

    public static void AddRabbitMq(this IServiceCollection services,
        IConfiguration configuration,
        Assembly assemblySource)
    {
        services.AddOptions<RabbitMqOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations();

        services.AddMassTransit(config =>
        {
            config.AddConsumers(assemblySource);
            config.AddActivities(assemblySource);
            config.SetKebabCaseEndpointNameFormatter();
            config.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
            {
                var rabbitConfig = provider.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                config.Host(new Uri(rabbitConfig.ConnectionString), hconfig =>
                {
                    hconfig.Username(rabbitConfig.Username);
                    hconfig.Password(rabbitConfig.Password);
                });

                config.ConfigureEndpoints(provider);
            }));
        });
    }

    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtOptions>()
            .Bind(configuration)
            .ValidateDataAnnotations();

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddAuthentication()
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidIssuer = configuration["JWT:ISSUER"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SECRET"]))
                };
            });
    }
}