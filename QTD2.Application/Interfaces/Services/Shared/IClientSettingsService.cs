using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ClientSettings;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IClientSettingsService
    {
        System.Threading.Tasks.Task<List<ClientSettings_Notification>> GetNotificationsAsync();

        System.Threading.Tasks.Task<ClientSettings_Notification> GetNotificationByNameAsync(string name);
        System.Threading.Tasks.Task UpdateNotificationAsync(string notification, ClientSettings_NotificationUpdateOptions options);

        System.Threading.Tasks.Task<ClientSettings_GeneralSettings> GetGeneralSettingsAsync();
        System.Threading.Tasks.Task UpdateGeneralSettingsAsync(ClientSettings_GeneralSettingsUpdateOptions options);

        System.Threading.Tasks.Task<List<ClientSettings_LabelReplacement>> GetLabelReplacementsAsync();
        System.Threading.Tasks.Task UpdateLabelReplacementsAsync(ClientSettings_LabelReplacementUpdateOptions options);

        System.Threading.Tasks.Task<ClientSettings_License> GetCurrentLicenseAsync();
        System.Threading.Tasks.Task<ClientSettings_LicenseVM> GetCurrentLicenseVMAsync();
        System.Threading.Tasks.Task<List<ClientSettings_LicenseVM>> GetLicenseHistoryAsync();
        System.Threading.Tasks.Task<ClientSettings_LicenseVM> UpdateLicenseAsync(ClientSettings_LicenseUpdateOptions options);
        System.Threading.Tasks.Task<ClientSettings_LicenseVM> AnalyzeLicenseAsync(ClientSettings_AnalyzeLicenseOptions options);
        System.Threading.Tasks.Task<List<ClientSettings_FeatureVM>> GetAllFeaturesAsync();
        System.Threading.Tasks.Task UpdateFeaturesAsync(ClientSettings_FeatureUpdateOptions options);
        List<ClientSettings_TimeZoneVM> GetAllTimeZonesAsync();
        Task<ClientSettings_FeatureVM> GetPublicClassFeaturesAsync();
    }
}
