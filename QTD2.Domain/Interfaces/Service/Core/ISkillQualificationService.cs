using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface ISkillQualificationService : Common.IService<SkillQualification>
    {
        System.Threading.Tasks.Task<SkillQualification> GetSkillQualificationAsync(int id);
        System.Threading.Tasks.Task<SkillQualification> GetSkillQualificationByScheduleAndEmployeeAsync(int? ClassScheduleId, int? employeeId, int? sequence);
        System.Threading.Tasks.Task<List<SkillQualification>> GetSkillQualificationByEmployeeIdAsync(int employeeId);
    }
}
