using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class SkillQualificationService : Common.Service<SkillQualification>, ISkillQualificationService
    {
        public SkillQualificationService(ISkillQualificationRepository repository, ISkillQualificationValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<SkillQualification> GetSkillQualificationAsync(int id)
        {
            var skillQualification = await FindAsync(r => r.Id == id);
            return skillQualification.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<SkillQualification> GetSkillQualificationByScheduleAndEmployeeAsync(int? ClassScheduleId, int? employeeId, int? sequence)
        {
            var sqs = await FindWithIncludeAsync(r => r.ClassScheduleId == ClassScheduleId && r.Sequence == sequence && r.EmployeeId == employeeId, new[] { "Employee" });
            return sqs.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<SkillQualification>> GetSkillQualificationByEmployeeIdAsync(int employeeId)
        {
            var skillQualification = await FindAsync(r => r.EmployeeId == employeeId);
            return skillQualification.ToList();
        }

        public async Task<List<SkillQualification>> GetPendingSkillQualificationsListAsTraineeByEmpId(int employeeId)
        {
            var pendingTQ = (await FindWithIncludeAsync(x => x.EmployeeId == employeeId && x.Active && x.IsReleasedToEMP && !x.IsRecalled, new string[] { "SkillQualification_Evaluator_Links" }));
            return pendingTQ.Where(x => x.IsPending).ToList();
        }
        public async Task<List<SkillQualification>> GetCompletedSkillQualificationsListAsEvalByEmpId(int employeeId)
        {
            return (await FindWithIncludeAsync(x => x.SkillQualification_Evaluator_Links.Any(y => y.EvaluatorId == employeeId), new string[] { "EnablingObjective", "EnablingObjective.EnablingObjective_Topic", "EnablingObjective.EnablingObjective_Category", "EnablingObjective.EnablingObjective_SubCategory" })).Where(x => x.IsComplete).ToList();
        }

        public async Task<List<SkillQualification>> GetCompletedSkillQualificationsListAsTraineeByEmpId(int employeeId)
        {
            return (await FindWithIncludeAsync(x => x.EmployeeId == employeeId, new string[] { "EnablingObjective.EnablingObjective_Topic", "EnablingObjective.EnablingObjective_Category", "EnablingObjective.EnablingObjective_SubCategory", "EnablingObjective.Position_SQs" })).Where(x => x.IsComplete).ToList();
        }

        public async Task<List<SkillQualification>> GetBySkillQualificationIDAsync(int id)
        {
            return (await FindWithIncludeAsync(x => x.Id == id, new string[] { "EnablingObjective" })).ToList();
        }
    }
}
