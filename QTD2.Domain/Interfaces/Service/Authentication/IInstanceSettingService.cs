using QTD2.Domain.Entities.Authentication;

namespace QTD2.Domain.Interfaces.Service.Authentication
{
    public interface IInstanceSettingService : Common.IService<InstanceSetting>
    {
        System.Threading.Tasks.Task<InstanceSetting> GetByInstanceNameAsync(string instanceName);
    }
}
