using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IClassScheduleEmployee_ILACertificationLink_PartialCreditService : Common.IService<ClassScheduleEmployee_ILACertificationLink_PartialCredit>
    {
        public System.Threading.Tasks.Task<List<ClassScheduleEmployee_ILACertificationLink_PartialCredit>> GetByILACertificationLinkIdAsync(int iLACertificationLinkId);
        public System.Threading.Tasks.Task<List<ClassScheduleEmployee_ILACertificationLink_PartialCredit>> GetByClassScheduleEmployeeIdsAsync(List<int> classEmpIds);
        public System.Threading.Tasks.Task<ClassScheduleEmployee_ILACertificationLink_PartialCredit> GetByILACertificationAndClassScheduleEmployeeIdAsync(int classEmpId, int ilaCertId);
        public System.Threading.Tasks.Task<List<ClassScheduleEmployee_ILACertificationLink_PartialCredit>> GetByClassScheduleEmployeeIdWithSubRequirementAsync(int classEmpId);
    }
}
