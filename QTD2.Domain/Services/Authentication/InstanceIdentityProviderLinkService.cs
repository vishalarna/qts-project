using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Repository.Authentication;
using QTD2.Domain.Interfaces.Service.Authentication;
using QTD2.Domain.Interfaces.Validation.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Authentication
{
    public class InstanceIdentityProviderLinkService : Common.Service<InstanceIdentityProviderLink>, IInstanceIdentityProviderLinkService
    {
        public InstanceIdentityProviderLinkService(IInstanceIdentityProviderLinkRepository repository, IInstanceIdentityProviderLinkValidation validation)
            : base(repository, validation)
        {
        }
    }
}
