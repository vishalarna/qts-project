using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Application.Interfaces.Services.Authentication;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Infrastructure.Model.Instance;
using IAuthenticationSettingDomainService = QTD2.Domain.Interfaces.Service.Authentication.IAuthenticationSettingService;

namespace QTD2.Application.Services.Authentication
{
    public class AuthenticationSettingService : IAuthenticationSettingService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IAuthenticationSettingDomainService _authenticationSettingDomainService;

        public AuthenticationSettingService(IHttpContextAccessor httpContextAccessor, IAuthorizationService authorizationService,
           IAuthenticationSettingDomainService authenticationSettingDomainService
           )

        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _authenticationSettingDomainService = authenticationSettingDomainService;
        }

        public async Task<AuthenticationSetting> GetAsync()
        {
            var authenticationSetting= await _authenticationSettingDomainService.AllAsync();
            return authenticationSetting.FirstOrDefault();
        }
    }
}
