using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IEmployeeCertificationService : Common.IService<EmployeeCertification>
    {
        //System.Threading.Tasks.Task<List<EmployeeCertification>> GetEmployeesCertificationHistoryAsync(string activeType);

        System.Threading.Tasks.Task<int> GetEmployeesWithExpiredCertificates();
        System.Threading.Tasks.Task<object> GetExpiredCertificates();


        System.Threading.Tasks.Task<List<EmployeeCertification>> GetEmployeesCertificationHistoryAsync(List<int> employees);
        System.Threading.Tasks.Task<List<EmployeeCertification>> GetEmployeeCertificationByEmployeeCertIds(List<int> employeeCertificationIds);
        System.Threading.Tasks.Task<List<EmployeeCertification>> GetActiveEmployeeCertificationsAsync();
        System.Threading.Tasks.Task<List<EmployeeCertification>> GetCertificationsExpiringAsync(DateTime expirationDate);
        Task<List<EmployeeCertification>> GetEmployeeCertificationsByEmployeeAndTypeAsync(List<int> employeeIds, List<int> certificationIds);
        System.Threading.Tasks.Task<List<EmployeeCertification>> GetActiveEmployeeNERCCertificationsAsync();
        Task<List<EmployeeCertification>> GetEmployeeCertificationsByEmployeeId(int empId);
    }
}
