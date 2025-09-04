using QTD2.Domain.Entities.Core;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class SkillQualificationEmpSettingService : Common.Service<SkillQualificationEmpSetting>, ISkillQualificationEmpSettingService
    {
        public SkillQualificationEmpSettingService(ISkillQualificationEmpSettingRepository repository, ISkillQualificationEmpSettingValidation validation)
           : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<SkillQualificationEmpSetting> GetSQSettingBySkillQualificationIdAsync(int skillQualificationId)
        {
            var skillQualificationEmpSetting = await FindAsync(r => r.SkillQualificationId == skillQualificationId);
            return skillQualificationEmpSetting.FirstOrDefault();
        }
    }
}
