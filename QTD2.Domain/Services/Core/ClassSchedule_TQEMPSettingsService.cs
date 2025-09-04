using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class ClassSchedule_TQEMPSettingsService : Common.Service<ClassSchedule_TQEMPSetting>, IClassSchedule_TQEMPSettingsService
    {
        public ClassSchedule_TQEMPSettingsService(IClassSchedule_TQEMPSettingsRepository repository, IClassSchedule_TQEMPSettingsValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<ClassSchedule_TQEMPSetting> GetTQEmpSettingsByClassId(int id)
        {
            return (await FindWithIncludeAsync(x => x.ClassScheduleId == id, new string[] { "ClassSchedule.ClassSchedule_Evaluator_Links" })).FirstOrDefault();
        }
    }
}