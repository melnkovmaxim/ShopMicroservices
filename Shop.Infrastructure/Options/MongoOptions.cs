using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace Shop.Infrastructure.Options;

public class MongoOptions
{
    [Required]
    [ConfigurationKeyName("MONGO:CONNECTION_STRING")]
    public string ConnectionString { get; set; } = null!;
    
    [Required]
    [ConfigurationKeyName("MONGO:DATABASE")]
    public string Database { get; set; } = null!;
}