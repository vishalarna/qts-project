using System.Collections.Generic;
using System.Security.Claims;

namespace QTD2.Infrastructure.Identity.Interfaces
{
    public interface IIdentity
    {
        ClaimsIdentity ClaimsIdentity { get; }

        string Username { get; }

        IEnumerable<Claim> Claims { get; }
    }
}
