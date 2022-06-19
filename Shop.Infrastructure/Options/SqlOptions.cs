using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace Shop.Infrastructure.Options;

public class SqlOptions
{
    [Required]
    [ConfigurationKeyName("SQL:CONNECTION_STRING")]
    public string ConnectionString { get; set; } = null!;
}