using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Common;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation;
using QTD2.Domain.Interfaces.Validation.Core;

namespace QTD2.Domain.Services.Core
{
    public class TQEmpSettingService : Common.Service<TQEmpSetting>, ITQEmpSettingService
    {
        public TQEmpSettingService(ITQEmpSettingRepository repository, ITQEmpSettingValidation validation)
            : base(repository, validation)
        {
        }
        public async Task<TQEmpSetting> GetTQEmpSettingByTQId(int taskQualificationId)
        {
            var taskEmp = (await FindAsync(x => x.TaskQualificationId == taskQualificationId)).FirstOrDefault();
            return taskEmp;
        }

        public async Task<List<TQEmpSetting>> GetTQEmpSettingByTQIds(List<int> tqIds)
        {
            var taskEmpSettings = (await FindAsync(x => tqIds.Contains(x.TaskQualificationId))).ToList();
            return taskEmpSettings;
        }
    }
}
