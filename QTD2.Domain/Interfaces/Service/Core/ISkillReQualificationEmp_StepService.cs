using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISkillReQualificationEmp_StepService : Common.IService<Entities.Core.SkillReQualificationEmp_Step>
    {
        public System.Threading.Tasks.Task<List<SkillReQualificationEmp_Step>> GetBySkillQualificationId(int skillQualificationId, int employeeId, int evaluatorId);
        public System.Threading.Tasks.Task<List<SkillReQualificationEmp_Step>> GetBySkillQualificationAndStepIdAsync(int skillQualificationId, int employeeId, int evaluatorId, int stepId);
        public System.Threading.Tasks.Task<List<SkillReQualificationEmp_Step>> GetStepsBySkillQualificationAndTraineeIdAsync(int skillQualificationId, int traineeId);
        public System.Threading.Tasks.Task<List<SkillReQualificationEmp_Step>> GetByStepIdAsync(int skillStepId);
    }
}
