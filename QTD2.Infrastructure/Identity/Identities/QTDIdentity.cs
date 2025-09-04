using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using QTD2.Infrastructure.Identity.Interfaces;

namespace QTD2.Infrastructure.Identity.Identities
{
    public class QTDIdentity : ClaimsPrincipal, IIdentity
    {
        public ClaimsIdentity ClaimsIdentity
        {
            get { return new ClaimsIdentity(Claims); }
        }

        public string Username
        {
            get { return Claims.Where(r => r.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value; }
        }

        public override IEnumerable<Claim> Claims { get; }

        public QTDIdentity(IEnumerable<Claim> claims)
        {
            Claims = claims;
        }
    }
}
