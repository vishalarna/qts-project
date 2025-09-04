using QTD2.Domain.Entities.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassSchedule_TQEMPSettingsService : Common.IService<ClassSchedule_TQEMPSetting>
    {
        public Task<ClassSchedule_TQEMPSetting> GetTQEmpSettingsByClassId(int id);
    }
}
