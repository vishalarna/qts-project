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
    public class SkillQualification_Evaluator_LinkService : Common.Service<SkillQualification_Evaluator_Link>, ISkillQualification_Evaluator_LinkService
    {
        public SkillQualification_Evaluator_LinkService(ISkillQualification_Evaluator_LinkRepository repository, ISkillQualification_Evaluator_LinkValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<List<SkillQualification_Evaluator_Link>> GetSkillQualificationEvaluatorLinkByIdAsync(int skillQualificationId)
        {
            var skillQualification = await FindAsync(r => r.SkillQualificationId == skillQualificationId);
            return skillQualification.ToList();

        }
    }
}
