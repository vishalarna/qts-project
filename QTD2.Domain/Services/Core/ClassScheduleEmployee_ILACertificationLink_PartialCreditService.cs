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
    public class ClassScheduleEmployee_ILACertificationLink_PartialCreditService : Common.Service<ClassScheduleEmployee_ILACertificationLink_PartialCredit>, IClassScheduleEmployee_ILACertificationLink_PartialCreditService
    {
        public ClassScheduleEmployee_ILACertificationLink_PartialCreditService(IClassScheduleEmployee_ILACertificationLink_PartialCreditRepository repository, IClassScheduleEmployee_ILACertificationLink_PartialCreditValidation validation)
            : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<List<ClassScheduleEmployee_ILACertificationLink_PartialCredit>> GetByILACertificationLinkIdAsync(int iLACertificationLinkId)
        {
            var partialCredits = await FindWithIncludeAsync(x => x.ILACertificationLinkId == iLACertificationLinkId, new string[] { "ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits" });
            return partialCredits.ToList();
        }

        public async System.Threading.Tasks.Task<List<ClassScheduleEmployee_ILACertificationLink_PartialCredit>> GetByClassScheduleEmployeeIdsAsync(List<int> classEmpIds)
        {
            return (await FindWithIncludeAsync(r => classEmpIds.Contains(r.ClassScheduleEmployeeId), new[] { "ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits" })).ToList();
        }
        public async System.Threading.Tasks.Task<ClassScheduleEmployee_ILACertificationLink_PartialCredit> GetByILACertificationAndClassScheduleEmployeeIdAsync(int classEmpId, int ilaCertId)
        {
            return (await FindWithIncludeAsync(r => r.ClassScheduleEmployeeId == classEmpId && r.ILACertificationLinkId == ilaCertId, new[] { "ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits" })).FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<ClassScheduleEmployee_ILACertificationLink_PartialCredit>> GetByClassScheduleEmployeeIdWithSubRequirementAsync(int classEmpId)
        {
            return (await FindWithIncludeAsync(r => r.ClassScheduleEmployeeId == classEmpId, new[] { "ILACertificationLink.ILACertificationSubRequirementLink.CertificationSubRequirement", "ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits.ILACertificationSubRequirementLink.CertificationSubRequirement" })).ToList();
        }
    }
}
