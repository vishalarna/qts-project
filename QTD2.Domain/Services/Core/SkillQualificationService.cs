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
    }
}
