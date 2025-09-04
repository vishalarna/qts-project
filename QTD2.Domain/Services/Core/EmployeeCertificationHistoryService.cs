using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class EmployeeCertificationHistoryService : Common.Service<EmployeeCertifictaionHistory>, IEmployeeCertificationHistoryService
    {
        public EmployeeCertificationHistoryService(IEmployeeCertificationHistoryRepository repository, IEmployeeCertificationHistoryValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<EmployeeCertifictaionHistory>> GetEmployeeCertificationsByEmployeeAndTypeAsync(List<int> employeeIds, List<int> certificationIds)
        {
            var employees = await FindWithIncludeAsync(r => employeeIds.Contains(r.EmployeeCertification.EmployeeId) && certificationIds.Contains(r.EmployeeCertification.CertificationId), new[] { "EmployeeCertification.Certification.CertificationSubRequirements", "EmployeeCertification.Certification.CertifyingBody", "EmployeeCertification.Employee.Person" });

            return employees.ToList();
        }
    }
}
