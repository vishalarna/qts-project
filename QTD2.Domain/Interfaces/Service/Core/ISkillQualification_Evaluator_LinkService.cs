using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISkillQualification_Evaluator_LinkService : Common.IService<SkillQualification_Evaluator_Link>
    {
        System.Threading.Tasks.Task<List<SkillQualification_Evaluator_Link>> GetSkillQualificationEvaluatorLinkByIdAsync(int skillQualificationId);
        Task<List<SkillQualification_Evaluator_Link>> GetPendingSkillQualificationsByEvaluator(int evaluatorId);
    }
}
