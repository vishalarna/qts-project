using System.Collections.Generic;
using System.Security.Claims;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Identity.Settings;

namespace QTD2.Infrastructure.Identity.Interfaces
{
    public interface IClaimsBuilder
    {
        List<Claim> Build(AppUser user, List<Claim> claims, ClaimsBuilderOptions options);
    }
}
