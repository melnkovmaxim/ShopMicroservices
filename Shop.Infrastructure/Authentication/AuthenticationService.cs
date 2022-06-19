using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shop.Infrastructure.Options;

namespace Shop.Infrastructure.Authentication;

public class AuthenticationService: IAuthenticationService
{
    private readonly JwtOptions _jwtOptions;

    public AuthenticationService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    
    public JwtToken CreateJwt(Guid userId)
    {
        var issuerSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var credentials = new SigningCredentials(issuerSignInKey, SecurityAlgorithms.HmacSha256);
        var identityClaims = new List<Claim>()
        {
            new (JwtRegisteredClaimNames.Sub, userId.ToString()),
            new (JwtRegisteredClaimNames.UniqueName, userId.ToString())
        };
        
        var now = DateTime.UtcNow;
        var descriptor = new SecurityTokenDescriptor()
        {
            Issuer = _jwtOptions.Issuer,
            IssuedAt = now,
            Expires = now.AddMinutes(_jwtOptions.TokenExpiryMinutes),
            Subject = new ClaimsIdentity(identityClaims),
            SigningCredentials = credentials
        };
        
        // var validationParameters = new TokenValidationParameters()
        // {
        //     ValidateAudience = false,
        //     ValidIssuer = _jwtOptions.Issuer,
        //     IssuerSigningKey = issuerSignInKey
        // };
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwt = jwtHandler.CreateToken(descriptor);
        var encodedJwt = jwtHandler.WriteToken(jwt);
        
        return new JwtToken()
        {
            EncodedJwt = encodedJwt,
            Expires = ((DateTimeOffset) jwt.ValidTo).ToUnixTimeSeconds()
        };
    }
}