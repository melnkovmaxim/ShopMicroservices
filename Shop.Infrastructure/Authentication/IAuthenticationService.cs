using System.IdentityModel.Tokens.Jwt;

namespace Shop.Infrastructure.Authentication;

public interface IAuthenticationService
{
    JwtToken CreateJwt(Guid userId);
}