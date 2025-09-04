using System.Collections.Generic;
using System.Security.Claims;
using QTD2.Domain;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Identity.Interfaces;
using QTD2.Infrastructure.Identity.Settings;

namespace QTD2.Infrastructure.Identity.ClaimsBuilders
{
    public class RefreshClaimsBuilder : IClaimsBuilder
    {
        public List<Claim> Build(AppUser user, List<Claim> claims, ClaimsBuilderOptions options)
        {
            List<Claim> nClaims = new List<Claim>();
            nClaims.AddRange(claims);
            nClaims.Add(new Claim(CustomClaimTypes.UserName, user.UserName));

            return nClaims;
        }
    }
}
