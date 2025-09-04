using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Repository;
using QTD2.Domain.Interfaces.Repository.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Data.Repository.Authentication
{
    public class InstanceIdentityProviderLinkRepository : Common.Repository<InstanceIdentityProviderLink>, IInstanceIdentityProviderLinkRepository
    {
        public InstanceIdentityProviderLinkRepository(QTDAuthenticationContext context)
            : base(context)
        {
        }
    }
}
