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
    public class SkillReQualificationEmp_QuestionAnswerService : Common.Service<Entities.Core.SkillReQualificationEmp_QuestionAnswer>, Interfaces.Service.Core.ISkillReQualificationEmp_QuestionAnswerService
    {
        public SkillReQualificationEmp_QuestionAnswerService(ISkillReQualificationEmp_QuestionAnswerRepository skillReQualificationEmp_QuestionAnswerRepository, ISkillReQualificationEmp_QuestionAnswerValidation skillReQualificationEmp_QuestionAnswerValidation)
            : base(skillReQualificationEmp_QuestionAnswerRepository, skillReQualificationEmp_QuestionAnswerValidation)
        {
        }

        public async System.Threading.Tasks.Task<List<SkillReQualificationEmp_QuestionAnswer>> GetBySkillQualificationIdAsync(int skillQualificationId, int employeeId, int evaluatorId)
        {
            return (await FindWithIncludeAsync(x => x.SkillQualificationId == skillQualificationId && x.TraineeId == employeeId && x.EvaluatorId == evaluatorId, new string[] { "SkillQualification.EnablingObjective.EnablingObjective_Questions" })).ToList();
        }

        public async System.Threading.Tasks.Task<List<SkillReQualificationEmp_QuestionAnswer>> GetBySkillQualificationAndQuestionIdIdAsync(int skillQualificationId, int employeeId, int evaluatorId, int questionId)
        {
            return (await FindAsync(x => x.SkillQualificationId == skillQualificationId && x.TraineeId == employeeId && x.EvaluatorId == evaluatorId && x.SkillQuestionId == questionId)).ToList();
        }

        public async System.Threading.Tasks.Task<List<SkillReQualificationEmp_QuestionAnswer>> GetBySkillQualificationAndTraineeIdAsync(int skillQualificationId, int traineeId)
        {
            return (await FindWithIncludeAsync(x => x.SkillQualificationId == skillQualificationId && x.TraineeId == traineeId, new string[] { "EnablingObjective_Question" })).ToList();
        }

        public async System.Threading.Tasks.Task<List<SkillReQualificationEmp_QuestionAnswer>> GetByQuestionIdAsync(int skillQuestionId)
        {
            return (await FindAsync(x => x.SkillQuestionId == skillQuestionId)).ToList();
        }
    }
}
