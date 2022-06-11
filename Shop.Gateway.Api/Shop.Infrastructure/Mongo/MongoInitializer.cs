using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Shop.Infrastructure.Mongo;

public static class MongoInitializer
{
    private const string CONVENTION_REGISTRY_NAME = "Shop";
    
    public static void Initialize(IMongoDatabase db)
    {
        var conventionPack = new ConventionPack()
        {
            new IgnoreExtraElementsConvention(true),
            new CamelCaseElementNameConvention(),
            new EnumRepresentationConvention(BsonType.String)
        };
        
        ConventionRegistry.Register(CONVENTION_REGISTRY_NAME, conventionPack, f => true);
    }
}