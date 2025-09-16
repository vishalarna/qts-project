using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IEmployeeService : IService<Employee>
    {
        System.Threading.Tasks.Task<List<Employee>> GetActiveEmployeesAsync();
        System.Threading.Tasks.Task<List<Employee>> GetListOfCertifiedOperatorsAsync(List<int> organizationIDs, string active);
        System.Threading.Tasks.Task<string> GetEmployeeNameByIdAsync(int employeeId);
        Task<Employee> GetEmployeeByUsernameAsync(string username);
        Task<int> GetTotalEmployeeTrainees();
        public Task<int> GetEmployeesWithNoCertificate();
        public Task<int> GetEmployeesWithExpiredCertificates();

        public Task<List<Employee>> GetEmployeesWithCertificationsAsync();

        System.Threading.Tasks.Task<List<Employee>> GetEmployeesWithPersonAsync();

        System.Threading.Tasks.Task<List<Employee>> GetEmployeesWithOrganizationAsync();

        System.Threading.Tasks.Task<List<Employee>> GetEmployeeTrainingNeedsAssessmentAsync(List<int> employee);
        System.Threading.Tasks.Task<List<Employee>> GetAllEmployeesWithPositionsAsync();
        //flyins list
        public Task<List<Employee>> GetListEmployeeTrainees();

        public Task<List<Employee>> GetListEmployeesWithNoCertificate();

        public Task<List<Employee>> GetListEmployeesWithExpiredCertificates();

        public Task<List<Employee>> GetListOfTaskEvalatorsAsync(List<int> evaluatorsToFilter);
        Task<List<Employee>> GetEmployeesByListOfIds(List<int> list, bool includeTrainees);
        public Task<List<Employee>> GetAllEmployeesWithCompactPersons();

        public Task<List<Employee>> GetAllActiveEmployeesWithCompactPersons();

        public Task<Employee> GetEmployeesWithCompactPersons(int empId);
        System.Threading.Tasks.Task<Employee> GetWithPersonAsync(int employeeId);

        public System.Threading.Tasks.Task<List<Employee>> GetActiveEmployeesWithEmpIdsAndNameAsync();

        public System.Threading.Tasks.Task<Employee> GetEmployeeById(int employeeId);

       public  System.Threading.Tasks.Task<List<Employee>> GetEmployeeDetailsWithPerson();
       public System.Threading.Tasks.Task<List<Employee>> GetEmployeesWithEOPHoursAsync(List<int> employees);
       public System.Threading.Tasks.Task<List<Employee>> GetEmployeesByListOfEmpIds(List<int> empIds);
       public System.Threading.Tasks.Task<List<Employee>> GetEmployeeTrainingStatusAsync(List<int> employees, DateTime startDate, DateTime endDate);
       public System.Threading.Tasks.Task<List<Employee>> GetEmployeesListWithOrgAndPosAsync();
       public Task<List<Employee>> GetEmployeessForTasksMetbyEmployee(List<int> employeeIds, bool currentPositionsOnly, bool includeTrainees);
        public Task<List<Employee>> GetEmployeesForEmployeeActiveInactiveHistory(List<int> employeeIds);
        public System.Threading.Tasks.Task<Employee> GetEmployeeByClassScheduleEmployeeIdAsync(int cseId);
        public System.Threading.Tasks.Task<List<Employee>> GetEmployeesForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(List<int> employeeIds);
        public System.Threading.Tasks.Task<List<Employee>> GetEmployeesPersonDetailsByEmpIds(List<int> empIds);
        public System.Threading.Tasks.Task<List<Employee>> GetEmployeesForEmployeeTaskQualificationDatesByTaskGenerator(List<int> empIds, string activeInactiveAllEmployees, bool includeTrainees);
        public System.Threading.Tasks.Task<List<Employee>> GetEmployeesForEmployeeTaskQualificationRecordsForGivenPositionGenerator(List<int> employeeIds, bool includeTrainees);
        public System.Threading.Tasks.Task<List<Employee>> GetEmployeesWithPersonForTQEvaluatorRoleAsync();
        public System.Threading.Tasks.Task<List<Employee>> GetEmployeesAndPositionsByIdAsync(int empId, int positionId);
        public System.Threading.Tasks.Task<List<Employee>> GetEmployeesForSummaryOfTaskQualificationBySubDutyAreaGeneratorAsync(List<int> employeeIds);
        public System.Threading.Tasks.Task<List<Employee>> GetWithCertifications(List<int> employeeIds);
        public System.Threading.Tasks.Task<List<Employee>> GetCertifiedEmployeesforGivenCertificateAsync(List<int> employeeIds, string companyEmployeeStatus);
        public System.Threading.Tasks.Task<Employee> GetEmployeeWithOrgAndPosAsync(int employeeId);
        public System.Threading.Tasks.Task<List<Employee>> GetEmployeesByIdsAsync(List<int> employeeIds, bool includeTrainees);
        public System.Threading.Tasks.Task<List<Employee>> GetEmployeesWithPersonPositionCertificationsAsync(List<int> employeeIds);
        public System.Threading.Tasks.Task<Employee> GetEmployeeByPersonId(int personId);
        public Task<Employee> GetEmployeeByIdAsync(int id);
    }
}
