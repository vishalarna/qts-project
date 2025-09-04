using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Model.Instance;

namespace QTD2.Application.Interfaces.Services.Authentication
{
    public interface IInstanceService
    {
        Task<Instance> GetAsync(string name);

        Task<List<Instance>> GetAsync(bool onlyActive = false);

        Task<Instance> CreateAsync(InstanceCreateOptions options);

        Task<Instance> UpdateAsync(string name, InstanceUpdateOptions options);

        Task DeleteAsync(string name);

        Task<string> CreateDatabaseAsync(string name, DatabaseCreateOptions options);

        Task<InstanceSetting> GetInstanceSettingsAsync(string name);

        Task<List<InstanceSetting>> GetAllInstanceSettingAsync();

        Task<List<InstanceSetting>> GetAllActiveInstanceSettingAsync();

        Task UpdateDatabaseAsync(string name, DatabaseUpdateOptions options);

        Task<InstanceSetting> GetInstanceSettingsByScormTenantAsync(string tenantName);

        Task<Client> GetClientByInstanceNameAsync(string instanceName);

        Task<List<Instance>> GetAllInstancesWithInstanceSettingsAsync();
        Task<InstanceSetting> UpdateInstanceSettingAsync(string name, PublicInstanceSettingUpdatOptions options);
    }
}
