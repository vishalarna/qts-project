using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QTD2.Infrastructure.JWT
{
    public interface IJWTBuilder
    {
        JwtSecurityToken CreateJWTToken(List<Claim> claims, JWTOptions options);

        string CreateJWTTokenString(List<Claim> claims, JWTOptions options);
    }
}
