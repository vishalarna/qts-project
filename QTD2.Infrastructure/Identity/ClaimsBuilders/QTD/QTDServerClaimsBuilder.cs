using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Identity.Interfaces;
using QTD2.Infrastructure.Identity.Settings;

namespace QTD2.Infrastructure.Identity.ClaimsBuilders
{
    public class QTDServerClaimsBuilder : IClaimsBuilder
    {
        public List<Claim> Build(AppUser user, List<Claim> claims, ClaimsBuilderOptions options)
        {
            List<Claim> nClaims = new List<Claim>();

            nClaims.AddRange((IEnumerable<Claim>)claims.GroupBy(r => r.Type).Select(r => r.First()));
            nClaims.Add(new Claim(ClaimTypes.Name, user.UserName));

            // add current company claim
            // verify it against CU table

            // add Epmloyee Claim if exists

            // add ClientAdmin claim if exists
            return nClaims;
        }
    }
}
