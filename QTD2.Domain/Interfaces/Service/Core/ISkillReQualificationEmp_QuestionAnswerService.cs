using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISkillReQualificationEmp_QuestionAnswerService : Common.IService<SkillReQualificationEmp_QuestionAnswer>
    {
        public System.Threading.Tasks.Task<List<SkillReQualificationEmp_QuestionAnswer>> GetBySkillQualificationIdAsync(int skillQualificationId, int employeeId, int evaluatorId);
        public System.Threading.Tasks.Task<List<SkillReQualificationEmp_QuestionAnswer>> GetBySkillQualificationAndQuestionIdIdAsync(int skillQualificationId, int employeeId, int evaluatorId, int questionId);
        public System.Threading.Tasks.Task<List<SkillReQualificationEmp_QuestionAnswer>> GetBySkillQualificationAndTraineeIdAsync(int skillQualificationId, int traineeId);
        public System.Threading.Tasks.Task<List<SkillReQualificationEmp_QuestionAnswer>> GetByQuestionIdAsync(int skillQuestionId);
    }
}
