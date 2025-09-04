using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.Employee;
using QTD2.Infrastructure.Model.EmployeeCertification;
using QTD2.Infrastructure.Model.EmployeeOrganization;
using QTD2.Infrastructure.Model.EmployeePosition;
using System;
using QTD2.Domain.Services.Core;
using QTD2.API.Infrastructure.Model.Client;
using QTD2.Infrastructure.Model.Person;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetAsync();
        public Task<List<EmployeeListDTO>> GetEmployeeListAsync();

        public Task<List<Employee>> GetAllActiveEmployees();

        public Task<Employee> GetAsync(int id);

        public Task<List<Employee>> GetAllEmpWithPosAndOrgAsync();

        public Task<List<Employee>> GetAllEmpWithPosAndOrgIdsOnlyAsync();

        public Task<Employee> UpdateAsync(int id, EmployeeUpdateOptions options);

        public Task<Employee> CreateAsync(EmployeeCreateOptions options, bool isReturnConflictExp = false);

        public System.Threading.Tasks.Task DeleteAsync(int id);

        public System.Threading.Tasks.Task DeactivateAsync(int id, EmployeeOptions options);

        public System.Threading.Tasks.Task ActivateAsync(int id);
        public Task<Employee> DeactivateAsync(int id);

        public Task<EmployeeCertification> GetCertificationsAsync(int id);

        public Task<EmployeeCertification> GetCertificationsLinkAsync(int certId,int empId);

        public Task<EmployeeCertification> GetEmployeeCertificationFromHistory(int id);

        public Task<List<EMPCertificationVM>> GetCertificationsLinkedToEmployee(int id, string filter);

        public Task<Employee> AddCertificationAsync(int employeeId, EmployeeCertificateCreateOptions options);
        public Task<Employee> GetEmployeeByUsernameAsync(string username);
        public Task<EmployeeCertification> EditCertificationAsync(int id, EmployeeCertificateUpdateOptions options);

        public System.Threading.Tasks.Task DeleteCertificationAsync(int id);

        public Task<List<EmployeePosition>> GetPositionsAsync(int employeeId, string filter);

        public Task<EmployeePosition> AddPositionAsync(int employeeId, EmployeePositionCreateOptions options);

        public Task<EmployeePosition> EditPositionAsync(int employeeId, int positionId, EmployeePositionUpdateOptions options);

        public Task<EmployeePosition> DeletePositionAsync(int employeeId, int positionId,int empPosId);

        public Task<List<EmployeeOrganization>> GetOrganizationsAsync(int employeeId);

        //public Task<EmployeeOrganization> AddOrganizationAsync(int employeeId, EmployeeOrganizationCreateOptions options);

        public Task<EmployeeOrganization> EditOrganizationAsync(int employeeId, int organizationId, EmployeeOrganizationUpdateOptions options);

        public System.Threading.Tasks.Task DeleteOrganizationAsync(int employeeId, int organizationId);

        public Task<Employee> LinkOrganization(int empId, EmployeeOrganizationCreateOptions options);

        public Task<List<EmployeeOrganization>> GetOrganizationssEmployeeIsLinkedTo(int id);
        public System.Threading.Tasks.Task ToggleIsManagerAsync(int employeeId, EmployeeOrganizationCreateOptions options);

        public System.Threading.Tasks.Task UploadEmployeeFileAsync(int id, EmployeeDocumentOptions file);

        public Task<List<EmployeeDocument>> getUploadedFiles(int id);

        public Task<EmployeeWithPositionVM> GetEmployeeWithPositionAsync(int id);

        public Task<List<Employee>> GetAllEvaluatorsAsync();
        public Task<List<EmployeeNameOnlyVM>> GetAllEvaluatorsNamesAsync();


        public Task<List<Employee>> GetOnlyEmployeesAsync();

        public Task<EmployeePosition> GetPositionsByPositionaAndEmployeeIdAsync(int employeeId, int positionId, int empPosId);

        public Task<EmployeeCertification> RenewCertificationAsync(int id, EmployeeCertificateUpdateOptions options);

        //public Task<Employee> CertificationRequired(int employeeId, bool certRequired);
        public Task<Employee> GetByPersonIdAsync(int id);

        public Task<EmployeeDocument> DownloadFile(int id,int fileId);

        //public System.Threading.Tasks.Task UpdateEmployeeInactiveDate(int employeeId, DateTime inactiveDate, string reason);
        public Task<object> GetExpiredCertificates();

        public Task<string> GetEmpImageAsync(string name);

        public Task<string> GetEmpUserName();

        public Task<List<Employee>> GetEmployeesList();
        public Task<List<EmployeeNameOnlyVM>> GetEmployeesListNamesOnly();
        public Task<List<EmployeeListDTO>> GetEmployeesListWithOrgAndPosAsync();

        public System.Threading.Tasks.Task SaveIdpReviewAsync(int id, IDPReviewUpdateOptions options);
        public Task<Employee> GetEmployeeByNumberAsync(string employeeNumber);
        public Task<bool> GetEmployeeActiveStatusByEmpId(int empId);
        public Task<Employee> GetEmployeeByClassScheduleEmployeeAsync(int cseId);
        public Task<List<EmployeeCertification>> GetCertificationsByEmpIdAsync(int empId);
    }
}
