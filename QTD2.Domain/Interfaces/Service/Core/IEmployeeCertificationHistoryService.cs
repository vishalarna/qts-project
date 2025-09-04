using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IEmployeeCertificationHistoryService : IService<EmployeeCertifictaionHistory>
    {
        Task<List<EmployeeCertifictaionHistory>> GetEmployeeCertificationsByEmployeeAndTypeAsync(List<int> employeeIds, List<int> certificationIds);
    }
}
