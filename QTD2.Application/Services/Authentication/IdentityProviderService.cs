using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QTD2.Application.Interfaces.Services.Authentication;
using QTD2.Domain;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Model.IdentityProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IIdentityProviderDomainService = QTD2.Domain.Interfaces.Service.Authentication.IIdentityProviderService;
using IInstanceDomainService = QTD2.Domain.Interfaces.Service.Authentication.IInstanceService;

namespace QTD2.Application.Services.Authentication
{
    public class IdentityProviderService : IIdentityProviderService
    {
        private readonly IIdentityProviderDomainService _identityProviderService;
        private readonly IInstanceDomainService _instanceDomainService;
        private readonly UserManager<AppUser> _userManager;
        public IdentityProviderService(IIdentityProviderDomainService identityProviderService, UserManager<AppUser> userManager, IInstanceDomainService instanceDomainService)
        {
            _identityProviderService = identityProviderService;
            _userManager = userManager;
            _instanceDomainService = instanceDomainService;
        }
        public async Task<IdentityProviderVM> GetIdentityProviderByNameAsync(string name)
        {
            var identityProvider = await _identityProviderService.GetIdentityProviderByNameAsync(name);
            var result = new IdentityProviderVM(identityProvider.Id,identityProvider.Name,identityProvider.Type);
            return result;
        }

        public async Task<IdentityProviderVM> GetUserIdentityProviderByUsername(string username)
        {
            var identityProvider = await GetUserIdentityProviderEntityByUsername(username);
            var result = identityProvider != null ? new IdentityProviderVM(identityProvider.Id,identityProvider.Name,identityProvider.Type) : null;
            return result;
        }

        public async Task<IdentityProvider> GetUserIdentityProviderEntityByUsername(string username)
        {
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null)
            {
                return null;
            }
            var claims = await _userManager.GetClaimsAsync(user);
            var identityProviderClaim = claims.Where(r => r.Type == CustomClaimTypes.HasIdentityProvider).FirstOrDefault();
            if (identityProviderClaim == null)
            {
                return null;
            }
            else
            {
                var identityProviderName = identityProviderClaim.Value;
                var identityProvider = await _identityProviderService.GetIdentityProviderByNameAsync(identityProviderName);
                return identityProvider;
            }
        }

        public async Task UpdateUserIdentityProviderClaimByUsername(string username, IdentityProviderUpdateModel identityProviderUpdateModel)
        {
            var user = await _userManager.FindByNameAsync(username);
            var claims = await _userManager.GetClaimsAsync(user);
            var identityClaim = claims.Where(x => x.Type == CustomClaimTypes.HasIdentityProvider).FirstOrDefault();
            var newClaim = new Claim(CustomClaimTypes.HasIdentityProvider, identityProviderUpdateModel.IdentityProviderValue);
            if (identityClaim == null)
            {
                await _userManager.AddClaimAsync(user, newClaim);
            }
            else
            {
                await _userManager.ReplaceClaimAsync(user, identityClaim, newClaim);
            }
        }
    }
}
