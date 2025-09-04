using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class Dashboard_SettingService : Common.Service<DashboardSetting>, IDashboard_SettingService
    {
        public Dashboard_SettingService(IDashboard_SettingRepository repository, IDashboardSettingValidation validation)
              : base(repository, validation)
        {
        }
    }
}