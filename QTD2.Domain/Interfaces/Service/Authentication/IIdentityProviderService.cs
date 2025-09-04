using QTD2.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Authentication
{
    public interface IIdentityProviderService : Common.IService<IdentityProvider>
    {
        public Task<IdentityProvider> GetIdentityProviderByNameAsync(string name);
        public Task<IdentityProvider> GetIdentityProviderByIdAsync(int? id);
    }
}
