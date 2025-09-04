using QTD2.Domain.Entities.Authentication;
using QTD2.Domain.Interfaces.Repository.Authentication;
using QTD2.Domain.Interfaces.Service.Authentication;
using QTD2.Domain.Interfaces.Validation.Authentication;
using System.Linq;

namespace QTD2.Domain.Services.Authentication
{
    public class InstanceSettingService : Common.Service<InstanceSetting>, IInstanceSettingService
    {
        public InstanceSettingService(IInstanceSettingRepository repository, IInstanceSettingValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<InstanceSetting> GetByInstanceNameAsync(string instanceName)
        {
            var instances =  (await AllWithIncludeAsync(new[] { "Instance" })).ToList();
            return instances.Where(r => r.Instance.Name == instanceName).FirstOrDefault();
        }
    }
}
