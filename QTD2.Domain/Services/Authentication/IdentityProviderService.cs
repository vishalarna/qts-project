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
    public class IdentityProviderService : Common.Service<IdentityProvider>, IIdentityProviderService
    {
        public IdentityProviderService(IIdentityProviderRepository repository, IIdentityProviderValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<IdentityProvider> GetIdentityProviderByNameAsync(string name)
        {
            return (await FindAsync(x => x.Name == name)).FirstOrDefault();
        }

        public async Task<IdentityProvider> GetIdentityProviderByIdAsync(int? id)
        {
            return (await FindAsync(x => x.Id == id)).FirstOrDefault();
        }
    }
}
