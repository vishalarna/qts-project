using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace QTD2.Domain.Entities.Authentication
{
    public class AppUser : IdentityUser
    {
        public virtual List<AppClaim> Claims { get; set; }
    }
}
