using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class SkillReQualificationEmp_StepService : Common.Service<Entities.Core.SkillReQualificationEmp_Step>, Interfaces.Service.Core.ISkillReQualificationEmp_StepService
    {
        public SkillReQualificationEmp_StepService(ISkillReQualificationEmp_StepRepository skillReQualificationEmp_StepRepository, ISkillReQualificationEmp_StepValidation skillReQualificationEmp_StepValidation)
            : base(skillReQualificationEmp_StepRepository, skillReQualificationEmp_StepValidation)
        {
        }

        public async System.Threading.Tasks.Task<List<SkillReQualificationEmp_Step>> GetBySkillQualificationId(int skillQualificationId, int employeeId, int evaluatorId)
        {
            return (await FindWithIncludeAsync(x => x.SkillQualificationId == skillQualificationId && x.TraineeId == employeeId && x.EvaluatorId == evaluatorId, new string[] { "SkillQualification.EnablingObjective.EnablingObjective_Suggestions" })).ToList();
        }

        public async System.Threading.Tasks.Task<List<SkillReQualificationEmp_Step>> GetBySkillQualificationAndStepIdAsync(int skillQualificationId, int employeeId, int evaluatorId, int stepId)
        {
            return (await FindAsync(x => x.SkillQualificationId == skillQualificationId && x.TraineeId == employeeId && x.EvaluatorId == evaluatorId && x.SkillStepId == stepId)).ToList();
        }

        public async System.Threading.Tasks.Task<List<SkillReQualificationEmp_Step>> GetStepsBySkillQualificationAndTraineeIdAsync(int skillQualificationId, int traineeId)
        {
            return (await FindWithIncludeAsync(x => x.SkillQualificationId == skillQualificationId && x.TraineeId == traineeId, new string[] { "EnablingObjective_Step" })).ToList();
        }

        public async System.Threading.Tasks.Task<List<SkillReQualificationEmp_Step>> GetByStepIdAsync(int skillStepId)
        {
            return (await FindAsync(x => x.SkillStepId == skillStepId)).ToList();
        }
    }
}
