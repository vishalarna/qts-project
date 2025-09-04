using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace QTD2.Infrastructure.JWT
{
    public class JWTBuilder : IJWTBuilder
    {
        private readonly JWTSettings _jwtSettings;

        public JWTBuilder(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public JwtSecurityToken CreateJWTToken(List<Claim> claims, JWTOptions options)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.TokenSecretKey));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                expires: DateTime.UtcNow.AddMinutes(options.ExpirationMinutes),
                notBefore: DateTime.UtcNow,
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
            return token;
        }

        public string CreateJWTTokenString(List<Claim> claims, JWTOptions options)
        {
            return new JwtSecurityTokenHandler().WriteToken(CreateJWTToken(claims, options));
        }
    }
}
