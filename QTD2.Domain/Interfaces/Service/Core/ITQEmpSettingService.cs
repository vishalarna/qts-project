using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ITQEmpSettingService : Common.IService<TQEmpSetting>
    {
        public Task<TQEmpSetting> GetTQEmpSettingByTQId(int taskQualificationId);
        public Task<List<TQEmpSetting>> GetTQEmpSettingByTQIds(List<int> tqIds);
    }
}
