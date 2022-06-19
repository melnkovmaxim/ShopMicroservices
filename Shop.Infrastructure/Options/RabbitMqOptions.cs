using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace Shop.Infrastructure.Options;

public class RabbitMqOptions
{
    [Required]
    [ConfigurationKeyName("RABBITMQ:CONNECTION_STRING")]
    public string ConnectionString { get; set; } = null!;
    
    [Required]
    [ConfigurationKeyName("RABBITMQ:USERNAME")]
    public string Username { get; set; } = null!;
    
    [Required]
    [ConfigurationKeyName("RABBITMQ:PASSWORD")]
    public string Password { get; set; } = null!;
}