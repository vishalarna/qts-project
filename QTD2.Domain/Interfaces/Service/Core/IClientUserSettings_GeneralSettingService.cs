using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
  public  interface IClientUserSettings_GeneralSettingService: Common.IService<ClientSettings_GeneralSettings>
    {
        System.Threading.Tasks.Task<ClientSettings_GeneralSettings> GetGeneralSettings();
    }
}
