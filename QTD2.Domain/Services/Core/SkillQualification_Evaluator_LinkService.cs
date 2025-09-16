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

        public async Task<List<SkillQualification_Evaluator_Link>> GetPendingSkillQualificationsByEvaluator(int evaluatorId)
        {
            var sq_evals = await FindWithIncludeAsync(
                                                    r => r.EvaluatorId == evaluatorId && r.Active
                                                    && r.SkillQualification.IsReleasedToEMP && !r.SkillQualification.IsRecalled
                                                    && !r.SkillQualification.CriteriaMet
                                                    && !(r.SkillQualification.SkillQualificationEmp_SignOff.Where(s => s.EvaluatorId == evaluatorId && (s.IsCompleted ?? false)).Any())
                                                    && r.SkillQualification.SkillQualificationDate == null && r.SkillQualification.DueDate > DateTime.Now
                                                    ,
                                                new[] {
                                                "Evaluator.Person"
                                                , "SkillQualification.Employee.Person"
                                                , "SkillQualification.Employee.EmployeePositions.Position"
                                                , "SkillQualification.SkillQualificationEmpSetting"
                                                , "SkillQualification.SkillQualificationEmp_SignOff"
                                                , "SkillQualification.SkillQualificationStatus"
                                                });

            return sq_evals.Where(r => r.SkillQualification.IsPending).ToList();
        }
    }
}
