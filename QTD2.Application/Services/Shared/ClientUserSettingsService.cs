using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.ClientSettings;
using QTD2.Infrastructure.Model.ClientUserSettings;

namespace QTD2.Application.Services.Shared
{
    public class ClientUserSettingsService : IClientUserSettingsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ClientSettingsService> _localizer;
        private readonly IClientUserSettings_Dashboard_SettingService _clientUserDashboardSetting;

        private readonly UserManager<AppUser> _userManager;

        public ClientUserSettingsService(
                IHttpContextAccessor httpContextAccessor,
                IAuthorizationService authorizationService,
                IStringLocalizer<ClientSettingsService> localizer,
                IClientUserSettings_Dashboard_SettingService clientUserDashboardSetting,
                UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _clientUserDashboardSetting = clientUserDashboardSetting;
            _userManager = userManager;
        }

        public async Task<List<ClientUserSettings_DashboardSetting>> GetDashboardSettingsAsync()
        {
            var dashboardSettings_list = await _clientUserDashboardSetting.GetDashboardSettingsAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
            dashboardSettings_list = dashboardSettings_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, ClientUserSettings_Dashboard_SettingOperations.Read).Result.Succeeded).ToList();
            return dashboardSettings_list;
        }

        public async System.Threading.Tasks.Task UpdateDashboardSettingsAsync(CustomizeDashboardUpdateOptions options)
        {
            var username = _httpContextAccessor.HttpContext.User.Identity.Name;

            var dashboardSettings_list = await _clientUserDashboardSetting.GetDashboardSettingsAsync(username);
            if (dashboardSettings_list == null)
            {
                throw new ArgumentNullException();
            }


            if (options.Updates != null)
            {
                foreach (var obj in options.Updates)
                {
                    var dashboardSetting = dashboardSettings_list.Where(r => r.DashboardSetting.Name == obj.Settings).FirstOrDefault();

                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, dashboardSetting, ClientUserSettings_Dashboard_SettingOperations.Update);

                    if (!result.Succeeded)
                    {
                        throw new UnauthorizedAccessException();
                    }

                    if (obj.Enable)
                        dashboardSetting.Enable(username);

                    if (obj.Disable)
                        dashboardSetting.Disable(username);

                    var validationResult = await _clientUserDashboardSetting.UpdateAsync(dashboardSetting);
                }
            }
        }
    }
}
