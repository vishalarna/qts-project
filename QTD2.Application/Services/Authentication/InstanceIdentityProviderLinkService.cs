using QTD2.Application.Interfaces.Services.Authentication;
using QTD2.Infrastructure.Model.IdentityProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IInstanceDomainService = QTD2.Domain.Interfaces.Service.Authentication.IInstanceService;

namespace QTD2.Application.Services.Authentication
{
    public class InstanceIdentityProviderLinkService : IInstanceIdentityProviderLinkService
    {
        private readonly IInstanceDomainService _instanceService;

        public InstanceIdentityProviderLinkService(IInstanceDomainService instanceService)
        {
            _instanceService = instanceService;
        }
        public async Task<List<IdentityProviderVM>> GetInstanceIdentityProviderListByInstanceName(string name)
        {
            var instance = await _instanceService.GetInstancesWithIdentityProviderLinksByNamesAsync(name);
            var identityProviders = instance.InstanceIdentityProviderLinks
                .Where(iipl => iipl.Active && iipl.IdentityProvider.Active)
                .Select(link => new IdentityProviderVM
                {
                    Id = link.IdentityProvider.Id,
                    Name = link.IdentityProvider.Name,
                    Type = link.IdentityProvider.Type
                }).ToList();
            return identityProviders;
        }
    }
}
