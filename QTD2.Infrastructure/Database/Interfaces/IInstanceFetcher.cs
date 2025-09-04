using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Authentication;
using QTD2.Infrastructure.Model.Instance;

namespace QTD2.Infrastructure.Database.Interfaces
{
    public interface IInstanceFetcher
    {
        Task<InstanceSetting> GetInstanceSettingAsync(string name);
        Task<List<InstanceSetting>> GetAllInstancesAsync();
        Task<List<InstanceSetting>> GetAllActiveInstancesAsync();
    }
}
