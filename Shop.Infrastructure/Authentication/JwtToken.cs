namespace Shop.Infrastructure.Authentication;

public class JwtToken
{
    public string EncodedJwt { get; set; } = null!;
    public long Expires { get; set; }
}