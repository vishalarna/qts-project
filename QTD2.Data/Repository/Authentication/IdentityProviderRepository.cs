using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Repository.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Authentication
{
    public class IdentityProviderRepository : Common.Repository<IdentityProvider>, IIdentityProviderRepository
    {
        public IdentityProviderRepository(QTDAuthenticationContext context)
            : base(context)
        {
        }
    }
}
