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
    public class SkillReQualificationEmp_SuggestionService : Common.Service<SkillReQualificationEmp_Suggestion>, ISkillReQualificationEmp_SuggestionService
    {
        public SkillReQualificationEmp_SuggestionService(ISkillReQualificationEmp_SuggestionRepository repository, ISkillReQualificationEmp_SuggestionValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<List<SkillReQualificationEmp_Suggestion>> GetBySkillQualificationId(int skillQualificationId, int employeeId, int evaluatorId)
        {
            return (await FindWithIncludeAsync(x => x.SkillQualificationId == skillQualificationId && x.TraineeId == employeeId && x.EvaluatorId == evaluatorId, new string[] { "SkillQualification.EnablingObjective.EnablingObjective_Suggestions" })).ToList();
        }
    }
}
