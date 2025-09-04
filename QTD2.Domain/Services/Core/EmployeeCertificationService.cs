using Microsoft.EntityFrameworkCore;
using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class EmployeeCertificationService : Common.Service<EmployeeCertification>, IEmployeeCertificationService
    {
        public EmployeeCertificationService(IEmployeeCertificationRepository employeeCertificationRepository, IEmployeeCertificationValidation employeeCertificationValidation)
            : base(employeeCertificationRepository, employeeCertificationValidation)
        {

        }

        public async Task<List<EmployeeCertification>> GetActiveEmployeeCertificationsAsync()
        {
            return (await FindAsync(r => r.Active)).ToList();
        }

        public async Task<List<EmployeeCertification>> GetCertificationsExpiringAsync(DateTime expirationDate)
        {
            var certs = await FindWithIncludeAsync(r => r.ExpirationDate.HasValue && r.ExpirationDate < DateOnly.FromDateTime(expirationDate), new string[] { "Employee", "Certification" });
            return certs.ToList();
        }

        public async Task<List<EmployeeCertification>> GetEmployeeCertificationsByEmployeeAndTypeAsync(List<int> employeeIds, List<int> certificationIds)
        {
            var employees = await FindWithIncludeAsync(r => employeeIds.Contains(r.EmployeeId) && certificationIds.Contains(r.CertificationId), new[] { "Certification.CertificationSubRequirements", "Certification.CertifyingBody", "Employee.Person" });
            var employeesWithOrgs = await FindWithIncludeAsync(r => employeeIds.Contains(r.EmployeeId) && certificationIds.Contains(r.CertificationId), new[] { "Employee.EmployeeOrganizations.Organization" });
            var employeesWithPositions = await FindWithIncludeAsync(r => employeeIds.Contains(r.EmployeeId) && certificationIds.Contains(r.CertificationId), new[] { "Employee.EmployeePositions.Position" });

            foreach (var employee in employees)
            {
                employee.Employee.EmployeeOrganizations = employeesWithOrgs.Where(r => r.EmployeeId == employee.EmployeeId).First().Employee.EmployeeOrganizations;
                employee.Employee.EmployeePositions = employeesWithPositions.Where(r => r.EmployeeId == employee.EmployeeId).First().Employee.EmployeePositions;
            }

            return employees.ToList();
        }

        public async Task<List<EmployeeCertification>> GetEmployeesCertificationHistoryAsync(List<int> employeeIds)
        {
            List<Expression<Func<EmployeeCertification, bool>>> predicates = new List<Expression<Func<EmployeeCertification, bool>>>();

            if (employeeIds != null)
                predicates.Add(r => employeeIds.Contains(r.EmployeeId));

            var employees = await FindWithIncludeAsync(predicates, new[] { "Certification.CertifyingBody", "Employee.Person", "Certification", "EmployeeCertificationHistorys" });
            var employeesWithOrgs = await FindWithIncludeAsync(predicates, new[] { "Employee.EmployeeOrganizations.Organization" });
            var employeesWithPositions = await FindWithIncludeAsync(predicates, new[] { "Employee.EmployeePositions.Position" });

            foreach (var employee in employees)
            {
                employee.Employee.EmployeeOrganizations = employeesWithOrgs.Single(r => r.Id == employee.Id).Employee.EmployeeOrganizations.Where(eo => eo.Active).ToList();
                employee.Employee.EmployeePositions = employeesWithPositions.Single(r => r.Id == employee.Id).Employee.EmployeePositions.Where(ep => ep.Active).ToList();
            }

            return employees.ToList();
        }

        public async System.Threading.Tasks.Task<int> GetEmployeesWithExpiredCertificates()
        {
            var count = (await FindWithIncludeAsync(x => x.Employee != null && x.Employee.Person != null && x.Employee.Deleted == false && x.Employee.Person.Deleted == false && x.Employee.Active == true && x.Employee.Person.Active == true && x.ExpirationDate < DateOnly.FromDateTime(System.DateTime.Now) && x.Active == true && x.Deleted == false, new string[] { "Employee.Person" })).ToList();
            return count.Count;
        }
        public async System.Threading.Tasks.Task<object> GetExpiredCertificates()
        {
            var now = DateTime.UtcNow;
            var sixMonthsFromNow = DateOnly.FromDateTime(now.AddMonths(6));
            var result = await FindQueryWithIncludeAsync(x => x.ExpirationDate != null && x.ExpirationDate <= sixMonthsFromNow, new string[] { "Certification.CertifyingBody", "Employee.Person", "Employee.EmployeePositions.Position" }).Select(x => new
            {
                certificationId = x.CertificationId,
                employeecertificationId = x.Id,
                employeeName = x.Employee.Person.FirstName + " " + x.Employee.Person.LastName,
                empId = x.EmployeeId,
                position = String.Join(", ", x.Employee.EmployeePositions.Where(w => w.EndDate >= DateOnly.FromDateTime(DateTime.Now) || w.EndDate == null).Select(x => x.Position.PositionTitle)),
                expirationDate = x.ExpirationDate,
                certificationType = x.Certification.CertifyingBody.IsNERC ?? false ? "NERC" : "NON NERC",
                empactive = x.Employee.Active,
                personActive = x.Employee.Person.Active,

            }).Where(x => x.expirationDate != null && x.empactive == true && x.personActive == true).OrderBy(x => x.expirationDate).ToListAsync();
            return result;
        }

        public async Task<List<EmployeeCertification>> GetEmployeeCertificationByEmployeeCertIds(List<int> employeeCertificationIds)
        {
            List<Expression<Func<EmployeeCertification, bool>>> predicates = new List<Expression<Func<EmployeeCertification, bool>>>();
            predicates.Add(x => employeeCertificationIds.Contains(x.Id));
            return (await FindWithIncludeAsync(predicates, new[] { "Certification.CertificationSubRequirements", "Certification.CertifyingBody", "Employee.Person", "Employee.EmployeeOrganizations.Organization", "Employee.EmployeePositions.Position" })).ToList();
        }

        public async Task<List<EmployeeCertification>> GetActiveEmployeeNERCCertificationsAsync()
        {
            return (await FindAsync(r =>r.Certification.CertifyingBody.IsNERC.Value && r.Active)).ToList();
        }

        public async Task<List<EmployeeCertification>> GetEmployeeCertificationsByEmployeeId(int empId)
        {
            return (await FindAsync(r => r.EmployeeId == empId)).ToList();
        }
    }
}
