using System.Collections.Generic;
using System.Security.Claims;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Identity.Interfaces;
using QTD2.Infrastructure.Identity.Settings;

namespace QTD2.Infrastructure.Identity.ClaimsBuilders
{
    public class CloneClaimsBuilder : IClaimsBuilder
    {
        public List<Claim> Build(AppUser user, List<Claim> claims, ClaimsBuilderOptions options)
        {
            List<Claim> nClaims = new List<Claim>();

            foreach (var claim in options.CloneClaims)
            {
                nClaims.Add(claim);
            }

            nClaims.Add(new Claim(ClaimTypes.Name, user.UserName));

            return nClaims;
        }
    }
}
