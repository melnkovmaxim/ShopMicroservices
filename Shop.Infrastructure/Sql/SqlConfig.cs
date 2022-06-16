using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace Shop.Infrastructure.Sql;

public class SqlConfig
{
    [Required]
    [ConfigurationKeyName("SQL:CONNECTION_STRING")]
    public string ConnectionString { get; set; } = null!;
}