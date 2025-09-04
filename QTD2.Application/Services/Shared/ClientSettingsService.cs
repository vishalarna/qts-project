using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using QTD2.Infrastructure.HttpClients;
using QTD2.Application.Interfaces.Services.Shared;
using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Infrastructure.Authorization.Operations.Core;
using QTD2.Infrastructure.Model.ClientSettings;

using IClientSettings_NotificationDomainService = QTD2.Domain.Interfaces.Service.Core.IClientSettings_NotificationService;
using IEmployeeService = QTD2.Domain.Interfaces.Service.Core.IEmployeeService;
using QTD2.Infrastructure.HttpClients;
using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Infrastructure.Database.Interfaces;
using QTD2.Domain.Exceptions;

namespace QTD2.Application.Services.Shared
{
    public class ClientSettingsService : IClientSettingsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorizationService _authorizationService;
        private readonly IStringLocalizer<ClientSettingsService> _localizer;
        private readonly IClientSettings_NotificationDomainService _clientSettingsService;
        private readonly IClientUserSettings_GeneralSettingService _clientGeneralSettingService;
        private readonly IClientSettings_LabelReplacementsService _clientLableReplacementService;
        private readonly IClientSettings_LicenseService _clientLicenseService;
        private readonly IEmployeeService _employeeService;
        private readonly UserManager<AppUser> _userManager;
        private readonly QtdAuthenticationService _qtdAuthenticationService;
        private readonly IClientSettings_FeatureService _clientSettings_FeatureService;

        public ClientSettingsService(
                IHttpContextAccessor httpContextAccessor,
                IAuthorizationService authorizationService,
                IStringLocalizer<ClientSettingsService> localizer,
                IClientSettings_NotificationDomainService clientSettingsService,
                IClientUserSettings_GeneralSettingService clientGeneralSettingService,
                IClientSettings_LabelReplacementsService clientLableReplacementService,
                IClientSettings_LicenseService clientLicenseService,
                IEmployeeService employeeService,
                UserManager<AppUser> userManager,
                QtdAuthenticationService qtdAuthenticationService, IClientSettings_FeatureService clientSettings_FeatureService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
            _localizer = localizer;
            _clientSettingsService = clientSettingsService;
            _userManager = userManager;
            _clientGeneralSettingService = clientGeneralSettingService;
            _clientLableReplacementService = clientLableReplacementService;
            _clientLicenseService = clientLicenseService;
            _employeeService = employeeService;
            _qtdAuthenticationService = qtdAuthenticationService;
            _clientSettings_FeatureService = clientSettings_FeatureService;
        }

        public async Task<ClientSettings_GeneralSettings> GetGeneralSettingsAsync()
        {
            var generalSettings = await _clientGeneralSettingService.GetGeneralSettings();

            if (generalSettings != null)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, generalSettings, ClientUserSettings_GeneralSettingOperations.Read);
                if (result.Succeeded)
                {
                    return generalSettings;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            return generalSettings;
        }

        public async System.Threading.Tasks.Task UpdateGeneralSettingsAsync(ClientSettings_GeneralSettingsUpdateOptions options)
        {

            var generalSettings = await _clientGeneralSettingService.GetGeneralSettings();

            if (generalSettings == null)
            {
                throw new ArgumentNullException();
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, generalSettings, ClientUserSettings_GeneralSettingOperations.Update);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException();
            }


            if (!String.IsNullOrEmpty(options.CompanyName))
            {
                generalSettings.SetCompanyName(options.CompanyName);
            }
            if (!String.IsNullOrEmpty(options.CompanyLogo))
            {
                generalSettings.SetCompanyLogo(options.CompanyLogo);
            }
            if (!String.IsNullOrEmpty(options.DateFormat))
            {
                generalSettings.SetDateFormat(options.DateFormat);
            }
            if (!String.IsNullOrEmpty(options.DefaultTimeZone))
            {
                generalSettings.SetDefaultTimeZone(options.DefaultTimeZone);
            }
            if (!String.IsNullOrEmpty(options.ClassStartEndTimeFormat))
            {
                generalSettings.SetClassStartEndTimeFormat(options.ClassStartEndTimeFormat);
            }
            if (options.CompanySpecificCoursePassingScore > 0)
            {
                generalSettings.CompanySpecificCoursePassingScore = options.CompanySpecificCoursePassingScore;
            }

            var validationResult = await _clientGeneralSettingService.UpdateAsync(generalSettings);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }

        public async Task<List<ClientSettings_LabelReplacement>> GetLabelReplacementsAsync()
        {
            var labelReplacement_list = await _clientLableReplacementService.GetLabelReplacementAsync();
            labelReplacement_list = labelReplacement_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, ClientSettings_LabelReplacementsOperations.Read).Result.Succeeded).ToList();
            return labelReplacement_list;
        }

        public async System.Threading.Tasks.Task UpdateLabelReplacementsAsync(ClientSettings_LabelReplacementUpdateOptions options)
        {
            if (options.LabelReplacements != null)
            {
                foreach (var obj in options.LabelReplacements)
                {
                    var labelReplacement = await _clientLableReplacementService.GetByDefaultLabel(obj.DefaultLabel);
                    var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, labelReplacement, ClientSettings_LabelReplacementsOperations.Update);

                    if (!result.Succeeded)
                    {
                        throw new UnauthorizedAccessException();
                    }

                    labelReplacement.SetLabelReplacementText(obj.LabelReplacement);
                    labelReplacement.Modify(_httpContextAccessor.HttpContext.User.Identity.Name);

                    var validationResult = await _clientLableReplacementService.UpdateAsync(labelReplacement);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
            }
        }

        public async Task<ClientSettings_License> GetCurrentLicenseAsync()
        {
            var currentLicense = await _clientLicenseService.GetCurrentLicense();
            return currentLicense;
        }

        public async Task<ClientSettings_LicenseVM> GetCurrentLicenseVMAsync()
        {
            var currentLicense = await _clientLicenseService.GetCurrentLicense();

            if (currentLicense != null)
            {
                var result = _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, currentLicense, ClientSettings_LicenseOperations.Read).Result;
                if (result.Succeeded)
                {

                    ClientSettings_LicenseVM clientSettings_LicenseVM = new ClientSettings_LicenseVM
                    {
                        ActivationCode = currentLicense.ActivationCode,
                        ClientAccountNumber = currentLicense.ClientId,
                        Expiration = currentLicense.Expiration,
                        LicenseType = currentLicense.LicenseType,
                        TotalEmployeeRecordsAvailable = currentLicense.TotalEmployeeRecordsAvailable,
                        EmployeeRecordsUsed = currentLicense.EmployeeRecordsUsed,
                        HasEmp = currentLicense.HasEmp,
                        IsLicenseActiveAndValid = currentLicense.IsLicenseActiveAndValid,
                        RemainingEmployees = currentLicense.RemainingEmployees,
                        Deluxe = currentLicense.Deluxe,
                        Active = currentLicense.Active,
                        CreatedDate = currentLicense.CreatedDate,
                        Products = currentLicense.Products,
                    };
                    return clientSettings_LicenseVM;
                }
                else
                {
                    throw new UnauthorizedAccessException(message: _localizer["OperationNotAllowed"]);
                }
            }
            else
            {
                throw new QTDServerException("Current license not found.");
            }
        }

        public async System.Threading.Tasks.Task<ClientSettings_LicenseVM> UpdateLicenseAsync(ClientSettings_LicenseUpdateOptions options)
        {
            var currentLicense = await _clientLicenseService.GetCurrentLicense();
            var employees = await _employeeService.GetActiveEmployeesAsync();

            ClientSettings_License license = new ClientSettings_License(options.ClientAccountNumber, options.ActivationCode, employees);

            if (license.Validate())
            {
                await _clientLicenseService.AddAsync(license);

                if (currentLicense != null)
                {
                    currentLicense.Deactivate();
                    currentLicense.Modify(_httpContextAccessor.HttpContext.User.Identity.Name);
                    await _clientLicenseService.UpdateAsync(currentLicense);
                }

                ClientSettings_LicenseVM clientSettings_LicenseVM = new ClientSettings_LicenseVM
                {
                    ActivationCode = license.ActivationCode,
                    ClientAccountNumber = license.ClientId,
                    Expiration = license.Expiration,
                    LicenseType = license.LicenseType,
                    TotalEmployeeRecordsAvailable = license.TotalEmployeeRecordsAvailable,
                    EmployeeRecordsUsed = license.EmployeeRecordsUsed,
                    HasEmp = license.HasEmp,
                    IsLicenseActiveAndValid = license.IsLicenseActiveAndValid,
                    RemainingEmployees = license.RemainingEmployees,
                    Deluxe = license.Deluxe,
                    Active = license.Active,
                    CreatedDate = license.CreatedDate,
                    Products = license.Products,
                };
                return clientSettings_LicenseVM;
            }
            else
            {
                throw new QTDServerException("License Not Updated");
            }
        }
       
        public async System.Threading.Tasks.Task<ClientSettings_LicenseVM> AnalyzeLicenseAsync(ClientSettings_AnalyzeLicenseOptions options)
        {
            var employees = await _employeeService.GetActiveEmployeesAsync();
            ClientSettings_License license = new ClientSettings_License(options.ClientAccountNumber, options.ActivationCode, employees);
            license.Validate();
            if (license.Validate())
            {
                ClientSettings_LicenseVM clientSettings_LicenseVM = new ClientSettings_LicenseVM
                {
                    ActivationCode = license.ActivationCode,
                    ClientAccountNumber = license.ClientId,
                    Expiration = license.Expiration,
                    LicenseType = license.LicenseType,
                    TotalEmployeeRecordsAvailable = license.TotalEmployeeRecordsAvailable,
                    EmployeeRecordsUsed = license.EmployeeRecordsUsed,
                    HasEmp = license.HasEmp,
                    IsLicenseActiveAndValid = license.IsLicenseActiveAndValid,
                    RemainingEmployees = license.RemainingEmployees,
                    Deluxe = license.Deluxe,
                    Active = license.Active,
                    CreatedDate = license.CreatedDate,
                    Products = license.Products,
                };
                return clientSettings_LicenseVM;
            }
            else
            {
                throw new QTDServerException("License Analyze Failed");
            }
        }

        public async Task<List<ClientSettings_LicenseVM>> GetLicenseHistoryAsync()
        {
            var licenseHistory = await _clientLicenseService.GetLicenseHistoryAsync();
            var clientSettings_LicenseVMs = new List<ClientSettings_LicenseVM>();
            licenseHistory = licenseHistory.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, ClientSettings_FeatureOperations.Read).Result.Succeeded).ToList();
            foreach (var history in licenseHistory)
            {
                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, history, ClientSettings_FeatureOperations.Read);

                if (result.Succeeded)
                {
                    clientSettings_LicenseVMs.Add(new ClientSettings_LicenseVM
                    {
                        ActivationCode = history.ActivationCode,
                        ClientAccountNumber = history.ClientId,
                        Expiration = history.Expiration,
                        LicenseType = history.LicenseType,
                        TotalEmployeeRecordsAvailable = history.TotalEmployeeRecordsAvailable,
                        EmployeeRecordsUsed = history.EmployeeRecordsUsed,
                        HasEmp = history.HasEmp,
                        IsLicenseActiveAndValid = history.IsLicenseActiveAndValid,
                        RemainingEmployees = history.RemainingEmployees,
                        Deluxe = history.Deluxe,
                        Active = history.Active,
                        CreatedDate = history.CreatedDate,
                        Products = history.Products
                    });
                }
            }

            return clientSettings_LicenseVMs;
        }

        public async Task<List<ClientSettings_Notification>> GetNotificationsAsync()
        {
            var obj_list = await _clientSettingsService.GetClientNotificationSettings();
            obj_list = obj_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, ClientSettings_NotificationOperations.Read).Result.Succeeded).ToList();
            return obj_list;
        }
        public async Task<ClientSettings_Notification> GetNotificationByNameAsync(string name)
        {
            var obj = await _clientSettingsService.GetClientSettingNotificationByName(name);

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, ClientSettings_NotificationOperations.Read);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException();
            }

            return obj;
        }
        public async System.Threading.Tasks.Task UpdateNotificationAsync(string notificationName, ClientSettings_NotificationUpdateOptions options)

        {
            var notification = await _clientSettingsService.GetClientSettingNotificationByName(notificationName);
            var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.Identity.Name);

            if (notification == null)
            {
                throw new ArgumentNullException();
            }

            var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, notification, ClientSettings_NotificationOperations.Update);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException();
            }

            if (options.UpdateTemplates != null)
            {
                foreach (var template in options.UpdateTemplates)
                {
                    notification.UpdateTemplate(template.Order, template.Template, user.Id);
                }
            }

            if (options.AddRecipients != null)
            {
                foreach (var order in options.AddRecipients.Select(r => r.Order).Distinct())
                {
                    notification.AddRecipients(order, options.AddRecipients.Where(r => r.Order == order).Select(r => r.EmployeeId).ToList(), user.Id);
                }

            }

            if (options.RemoveRecipients != null)
            {
                foreach (var order in options.RemoveRecipients.Select(r => r.Order).Distinct())
                {
                    notification.RemoveRecipients(order, options.RemoveRecipients.Where(r => r.Order == order).Select(r => r.EmployeeId).ToList(), user.Id);
                }
            }

            if (options.NotificationCustomSettings != null)
            {
                foreach (var setting in options.NotificationCustomSettings)
                {
                    notification.UpdateCustomSetting(setting.Key, setting.Value, user.Id);
                }
            }

            if (options.NotificationStepCustomSettings != null)
            {
                foreach (var setting in options.NotificationStepCustomSettings)
                {
                    notification.UpdateStepCustomSetting(setting.StepOrder, setting.Key, setting.Value, user.Id);
                }
            }

            if (options.Disable)
            {
                notification.Disable(user.Id);
            }

            if (options.Enable)
            {
                notification.Enable(user.Id);
            }

            notification.Modify(_httpContextAccessor.HttpContext.User.Identity.Name);

            var validationResult = await _clientSettingsService.UpdateAsync(notification);
            if (!validationResult.IsValid)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
            }
        }

        public async Task<List<ClientSettings_FeatureVM>> GetAllFeaturesAsync()
        {
            var features_list = await _clientSettings_FeatureService.GetAllFeaturesAsync();
            features_list = features_list.Where(obj => _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, obj, ClientSettings_FeatureOperations.Read).Result.Succeeded).ToList();

            var features_data = new List<ClientSettings_FeatureVM>();

            foreach (var item in features_list)
            {
                var feature_data = new ClientSettings_FeatureVM()
                {
                    Id = item.Id,
                    Feature = item.Feature,
                    Enabled = item.Enabled
                };

                features_data.Add(feature_data);
            }

            return features_data;
        }

        public async System.Threading.Tasks.Task UpdateFeaturesAsync(ClientSettings_FeatureUpdateOptions options)
        {

            foreach(var opt in options.FeatureList)
            {               
                var featureModel = await _clientSettings_FeatureService.GetAsync(opt.Id);

                var result = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, featureModel, ClientSettings_FeatureOperations.Update);

                if (!result.Succeeded)
                {
                    throw new UnauthorizedAccessException();
                }

                if (featureModel.Enabled != opt.Enabled)
                {
                    featureModel.Enabled = opt.Enabled;
                    var validationResult = await _clientSettings_FeatureService.UpdateAsync(featureModel);

                    if (!validationResult.IsValid)
                    {
                        throw new System.ComponentModel.DataAnnotations.ValidationException(message: string.Join(',', validationResult.Errors));
                    }
                }
            }
        }

        public List<ClientSettings_TimeZoneVM> GetAllTimeZonesAsync()
        {
			var timeZones = ExtensionMethods.GetTimezones();
			var systemTimeZones = TimeZoneInfo.GetSystemTimeZones();
			var timeZoneModel = new List<ClientSettings_TimeZoneVM>();
			foreach (var timezone in timeZones)
			{
				var systemTimeZone = systemTimeZones.FirstOrDefault(x => x.Id == timezone.Timezone);
				if (systemTimeZone != null)
				{
					var isDst = systemTimeZone.IsDaylightSavingTime(DateTime.UtcNow);
					var timeZoneOffset = isDst ? systemTimeZone.GetUtcOffset(DateTime.UtcNow) : systemTimeZone.BaseUtcOffset;

					var timeZoneOffsetString = timeZoneOffset >= TimeSpan.Zero ? timeZoneOffset.ToString(@"\+hh\:mm") : timeZoneOffset.ToString(@"\-hh\:mm");
					var tz = new ClientSettings_TimeZoneVM()
					{
						Id = timezone.Timezone,
						DisplayName = $"(UTC {timeZoneOffsetString}) {(isDst ? systemTimeZone.DaylightName : systemTimeZone.StandardName)}",
					};
					timeZoneModel.Add(tz);
				}
			}

			return timeZoneModel;
		}

        public async Task<ClientSettings_FeatureVM> GetPublicClassFeaturesAsync()
        {
            var features_list = await _clientSettings_FeatureService.GetAllFeaturesAsync();
            var feature_data = features_list.Where(x => x.Feature == "Public Classes").FirstOrDefault();           
                var publicClass = new ClientSettings_FeatureVM()
                {
                    Id = feature_data.Id,
                    Feature = feature_data.Feature,
                    Enabled = feature_data.Enabled
                };
             return publicClass;
        }
    }
}
