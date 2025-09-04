using Microsoft.AspNetCore.Mvc;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Model.IdentityProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Authentication
{
    public interface IIdentityProviderService
    {
        public Task<IdentityProviderVM> GetIdentityProviderByNameAsync(string name);
        public Task<IdentityProviderVM> GetUserIdentityProviderByUsername(string username);
        public Task<IdentityProvider> GetUserIdentityProviderEntityByUsername(string username);
        public Task UpdateUserIdentityProviderClaimByUsername(string username, IdentityProviderUpdateModel identityProviderUpdateModel);
    }
}
