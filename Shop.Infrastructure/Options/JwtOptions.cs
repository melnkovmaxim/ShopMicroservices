using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Configuration;

namespace Shop.Infrastructure.Options;

public class JwtOptions
{
     [Required]
     [ConfigurationKeyName("JWT:SECRET")]
     public string Secret { get; set; } = null!;
     
     [Required]
     [ConfigurationKeyName("JWT:ISSUER")]
     public string Issuer { get; set; } = null!;
     
     [Required]
     [ConfigurationKeyName("JWT:EXPIRY_MINUTES")]
     public int TokenExpiryMinutes { get; set; }
}