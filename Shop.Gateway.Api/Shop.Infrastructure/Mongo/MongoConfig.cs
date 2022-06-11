using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace Shop.Infrastructure.Mongo;

public class MongoConfig
{
    [Required]
    [ConfigurationKeyName("CONNECTION_STRING")]
    public string ConnectionString { get; set; } = null!;
    
    [Required]
    [ConfigurationKeyName("DATABASE")]
    public string Database { get; set; } = null!;
}