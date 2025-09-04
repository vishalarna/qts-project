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
    public class SkillQualificationStatusService : Common.Service<SkillQualificationStatus>, ISkillQualificationStatusService
    {
        public SkillQualificationStatusService(ISkillQualificationStatusRepository repository, ISkillQualificationStatusValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<SkillQualificationStatus> GetSkillQualificationStatusByIdAsync(int? id)
        {
            var skillQualificationStatus = await FindAsync(r => r.Id == id);
            return skillQualificationStatus.FirstOrDefault();
        }
    }
}

