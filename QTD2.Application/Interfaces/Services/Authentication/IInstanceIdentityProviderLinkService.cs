using QTD2.Infrastructure.Model.IdentityProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Authentication
{
    public interface IInstanceIdentityProviderLinkService
    {
        public Task<List<IdentityProviderVM>> GetInstanceIdentityProviderListByInstanceName(string name);
    }
}
