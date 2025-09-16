using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;

namespace QTD2.Domain.Services.Core
{
    public class EmployeeService : Common.Service<Employee>, IEmployeeService
    {
        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeValidation validation)
            : base(employeeRepository, validation)
        {
        }
        public async Task<List<Employee>> GetActiveEmployeesAsync()
        {
            return (await FindAsync(r => r.Active && !r.PublicUser)).ToList();
        }

        public async Task<List<Employee>> GetEmployeeTrainingNeedsAssessmentAsync(List<int> employee)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();

            if (employee != null)
                predicates.Add(r => employee.Contains(r.Id));

            var employees = (await FindWithIncludeAsync(predicates, new[] { "EmployeePositions.Position", "ClassSchedule_Employee.ClassSchedule", "Person" })).ToList();

            return employees;
        }

        public async Task<string> GetEmployeeNameByIdAsync(int employeeId)
        {
            var employee = (await FindWithIncludeAsync(x => x.Id == employeeId, new string[] { "Person" })).FirstOrDefault();
            var employeeName = employee.Person.LastName + " " + employee.Person.FirstName;
            return employeeName;
        }

        public async Task<Employee> GetEmployeeByUsernameAsync(string username)
        {
            return (await FindAsync(r => r.Person.Username.ToUpper() == username.ToUpper())).First();
        }

        public async Task<int> GetTotalEmployeeTrainees()
        {
            var trainees = (await FindWithIncludeAsync(x => x.EmployeePositions.Where(s => s.Trainee == true).Count() > 0, new[] { "EmployeePositions" })).ToList();
            //return Max(trainees);
            return trainees.Count();
        }

        public async Task<int> GetEmployeesWithNoCertificate()
        {
            var employee = await FindQueryWithIncludeAsync(x => x.EmployeeCertifications.Count() == 0, new[] { "EmployeeCertifications" }).CountAsync();
            return employee;
        }

        public async Task<int> GetEmployeesWithExpiredCertificates()
        {
            var employee = (await FindWithIncludeAsync(x => x.Active == true && x.Person != null && x.Person.Active == true && x.EmployeeCertifications.Any(r => r.ExpirationDate < DateOnly.FromDateTime(System.DateTime.Now)) == true, new string[] { "Person", "EmployeeCertifications" })).ToList();
            return employee.Count();
        }



        //list of employees for flyins
        public async Task<List<Employee>> GetListEmployeeTrainees()
        {
            var trainees = (await FindWithIncludeAsync(x => x.EmployeePositions.Where(s => s.Trainee == true).Count() > 0, new string[] { "Person" })).Select(s => new Employee
            {
                Id = s.Id,
                EmployeeNumber = s.EmployeeNumber,
                Person = s.Person,
            }).ToList();
            return trainees;

        }

        public async Task<List<Employee>> GetListEmployeesWithNoCertificate()
        {
            var employee = (await FindWithIncludeAsync(x => x.EmployeeCertifications.Count() == 0, new string[] { "Person", "EmployeeCertifications" })).Select(s => new Employee
            {
                Id = s.Id,
                EmployeeNumber = s.EmployeeNumber,
                Person = s.Person,
            }).ToList();
            return employee;
        }

        public async Task<List<Employee>> GetListEmployeesWithExpiredCertificates()
        {
            var employee = (await FindWithIncludeAsync(x => x.Active == true && x.Person != null && x.Person.Active == true && x.EmployeeCertifications.Any(r => r.ExpirationDate < DateOnly.FromDateTime(System.DateTime.Now)) == true, new string[] { "Person", "EmployeeCertifications" })).ToList();
            return employee;
        }

        public async Task<List<Employee>> GetEmployeesWithCertificationsAsync()
        {
            var employees = await AllWithIncludeAsync(new[] { "EmployeeCertifications.Certification" });
            return employees.ToList();
        }
        public async Task<List<Employee>> GetEmployeesWithPersonAsync()
        {
            var employees = await AllWithIncludeAsync(new[] { "Person" });
            return employees.ToList();
        }
        public async Task<List<Employee>> GetEmployeesWithOrganizationAsync()
        {
            var employees = await AllWithIncludeAsync(new[] { "EmployeeOrganizations.Organization" });
            return employees.ToList();
        }
        public async Task<List<Employee>> GetAllEmployeesWithPositionsAsync()
        {
            var employees = await AllWithIncludeAsync(new[] { "EmployeePositions.Position" });
            return employees.ToList();
        }

        public async Task<List<Employee>> GetAllEmployeesWithCompactPersons()
        {
            var emps = (await FindWithIncludeAsync(x => x.Person != null, new string[] { "Person" })).Select(s => new Employee
            {
                Active = s.Active,
                Deleted = s.Deleted,
                Address = s.Address,
                City = s.City,
                EmployeeNumber = s.EmployeeNumber,
                Id = s.Id,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                ModifiedBy = s.ModifiedBy,
                ModifiedDate = s.ModifiedDate,
                Notes = s.Notes,
                InactiveDate = s.InactiveDate,
                Password = s.Password,
                PersonId = s.PersonId,
                PhoneNumber = s.PhoneNumber,
                Reason = s.Reason,
                State = s.State,
                TQEqulator = s.TQEqulator,
                WorkLocation = s.WorkLocation,
                ZipCode = s.ZipCode,
                Person = new Person
                {
                    Id = s.Person.Id,
                    Active = s.Person.Active,
                    Deleted = s.Person.Deleted,
                    Image = s.Person.Image,
                    FirstName = s.Person.FirstName,
                    LastName = s.Person.LastName,
                    MiddleName = s.Person.MiddleName,
                    Username = s.Person.Username
                }
            });

            return emps.OrderBy(o => o?.Person?.LastName).ToList();
        }


        public async Task<List<Employee>> GetAllActiveEmployeesWithCompactPersons()
        {
            var emps = await FindQueryWithIncludeAsync(x => x.Person != null && x.Active == true, new string[] { "Person" }).Select(s => new Employee
            {
                Active = s.Active,
                Deleted = s.Deleted,
                Address = s.Address,
                City = s.City,
                EmployeeNumber = s.EmployeeNumber,
                Id = s.Id,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                ModifiedBy = s.ModifiedBy,
                ModifiedDate = s.ModifiedDate,
                Notes = s.Notes,
                InactiveDate = s.InactiveDate,
                Password = s.Password,
                PersonId = s.PersonId,
                PhoneNumber = s.PhoneNumber,
                Reason = s.Reason,
                State = s.State,
                TQEqulator = s.TQEqulator,
                WorkLocation = s.WorkLocation,
                ZipCode = s.ZipCode,
                Person = new Person
                {
                    Id = s.Person.Id,
                    Active = s.Person.Active,
                    Deleted = s.Person.Deleted,
                    Image = s.Person.Image,
                    FirstName = s.Person.FirstName,
                    LastName = s.Person.LastName,
                    MiddleName = s.Person.MiddleName,
                    Username = s.Person.Username
                }
            }).OrderBy(o => o.Person.LastName).ToListAsync();

            return emps;
        }

        public async Task<Employee> GetEmployeesWithCompactPersons(int empId)
        {
            var emps = await FindQueryWithIncludeAsync(x => x.Id == empId, new string[] { "Person" }).Select(s => new Employee
            {
                Active = s.Active,
                Deleted = s.Deleted,
                Address = s.Address,
                City = s.City,
                EmployeeNumber = s.EmployeeNumber,
                Id = s.Id,
                CreatedBy = s.CreatedBy,
                CreatedDate = s.CreatedDate,
                ModifiedBy = s.ModifiedBy,
                ModifiedDate = s.ModifiedDate,
                Notes = s.Notes,
                InactiveDate = s.InactiveDate,
                Password = s.Password,
                PersonId = s.PersonId,
                PhoneNumber = s.PhoneNumber,
                Reason = s.Reason,
                State = s.State,
                TQEqulator = s.TQEqulator,
                WorkLocation = s.WorkLocation,
                ZipCode = s.ZipCode,
                Person = s.Person == null ? null : new Person
                {
                    Id = s.Person.Id,
                    Active = s.Person.Active,
                    Deleted = s.Person.Deleted,
                    FirstName = s.Person.FirstName,
                    LastName = s.Person.LastName,
                    MiddleName = s.Person.MiddleName,
                    Username = s.Person.Username
                }
            }).FirstOrDefaultAsync();

            return emps;
        }

        public async Task<List<Employee>> GetListOfTaskEvalatorsAsync(List<int> evaluatorsToFilter)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            if (evaluatorsToFilter != null)
                predicates.Add(r => evaluatorsToFilter.Contains(r.Id));
            var employees = await FindWithIncludeAsync(predicates, new[] { "EmployeeOrganizations.Organization", "EmployeePositions.Position", "Person" });
            return employees.ToList();
        }

        public async Task<List<Employee>> GetEmployeesByListOfIds(List<int> list, bool includeTrainees)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(r => list.Contains(r.Id));

            var employees = await FindWithIncludeAsync(predicates, new string[] { "Person", "EmployeePositions" });
            if (!includeTrainees)
            {
                foreach (var emp in employees)
                {
                    emp.EmployeePositions = emp.EmployeePositions.Where(p => p.Trainee == false).ToList();
                }
            }
            return employees.ToList();
        }

        public async Task<List<Employee>> GetEmployeesByListOfEmpIds(List<int> empIds)
        {
            return (await FindWithIncludeAsync(r => empIds.Contains(r.Id), new string[] { "Person", "EmployeePositions.Position" })).ToList();
        }

        public async Task<List<Employee>> GetListOfCertifiedOperatorsAsync(List<int> organizationIDs, string active)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            if (organizationIDs.Any())
            {
                predicates.Add(r=>r.EmployeeOrganizations.Any(eo=>organizationIDs.Contains(eo.OrganizationId)));
            }
            if (active.ToLower() == "inactive only")
            {
                predicates.Add(r => !r.Active);
            }
            else if (active.ToLower() == "active only")
            {
                predicates.Add(r => r.Active);
            }
            var employees = await FindWithIncludeAsync(predicates, new[]
            { "Person", "EmployeeOrganizations.Organization","EmployeeCertifications","EmployeeCertifications.Certification","EmployeePositions.Position" });

            return employees.ToList();
        }

        public async System.Threading.Tasks.Task<Employee> GetWithPersonAsync(int employeeId)
        {
            var employees = await FindWithIncludeAsync(r => r.Id == employeeId, new string[] { "Person" });
            return employees.FirstOrDefault();
        }

        public async Task<List<Employee>> GetActiveEmployeesWithEmpIdsAndNameAsync()
        {
            return (await FindWithIncludeAsync(r => r.Active, new string[] { "Person" })).ToList();
        }

        public async System.Threading.Tasks.Task<Employee> GetEmployeeById(int employeeId)
        {
            return (await FindAsync(r => r.Id == employeeId)).Select(s => new Employee { Id = s.Id, Active = s.Active, PersonId = s.PersonId, EmployeeNumber = s.EmployeeNumber }).FirstOrDefault();
        }

        public async Task<List<Employee>> GetEmployeeDetailsWithPerson()
        {
            var queryable = await FindWithIncludeAsync(x => x.Active, new string[] { "Person" });
            return queryable.ToList();
        }

        public async Task<List<Employee>> GetEmployeesWithEOPHoursAsync(List<int> employees)
        {
            var employeesList = (await FindWithIncludeAsync(x => employees.Contains(x.Id), new[] { "Person", "EmployeeOrganizations.Organization", "ClassSchedule_Employee.ClassSchedule.ILA.ILACertificationLinks.Certification" })).ToList();
            return employeesList;
        }

        public async System.Threading.Tasks.Task<List<Employee>> GetEmployeeTrainingStatusAsync(List<int> employeeIds, DateTime startDate, DateTime endDate)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            if (employeeIds.Count() > 0)
            {
                predicates.Add(r => employeeIds.Contains(r.Id));
            }

            var employees = await FindWithIncludeAsync(predicates, new[]
            {
                "Person",
                "EmployeePositions.Position",
                "EmployeeCertifications.Certification",
                "ClassSchedule_Employee.ClassSchedule"
            },true);

            var employeesWithClassSchedules = await FindWithIncludeAsync(predicates, new[]
            {
                "ClassSchedule_Employee.ClassSchedule.Location",
                "ClassSchedule_Employee.ClassSchedule.Instructor",
                "ClassSchedule_Employee.ClassSchedule.ClassSchedule_Employee",
                "ClassSchedule_Employee.ClassSchedule.ILA"
            },true);

            var employeesWithClassSchedulesIlas = await FindAsync(predicates);

            foreach (var employee in employeesWithClassSchedules)
            {
                foreach (var cse in employee.ClassSchedule_Employee)
                {
                    var test = employeesWithClassSchedulesIlas.Where(r => r.Id == employee.Id);
                    var test2 = test.SelectMany(r => r.ClassSchedule_Employee);
                    var test3 = test2.Select(r => r.ClassSchedule.ILA);
                    cse.ClassSchedule.ILA = test3.Where(r => r != null).Where(r => r.Id == cse.ClassSchedule.ILAID).FirstOrDefault();
                }
            }

            foreach (var employee in employees)
            {
                employee.ClassSchedule_Employee = employeesWithClassSchedules
                    .Where(r => r.Id == employee.Id).First()
                    .ClassSchedule_Employee
                    .Where(ep => ep.CompletionDate > startDate && ep.CompletionDate < endDate)
                    .Where(ep => !ep.Deleted)
                    .Where(ep => ep.Active)
                    .Where(ep => ep.IsEnrolled)
                    .OrderBy(x => x.CompletionDate).ToList();
                employee.EmployeePositions = employee.EmployeePositions.Where(x => x.Active == true).ToList();
            }
            return employees.ToList();
        }

        public async Task<List<Employee>> GetEmployeesListWithOrgAndPosAsync()
        {
            var employeesList = (await FindWithIncludeAsync(x => x.Active == true, new[] { "Person", "EmployeePositions.Position", "EmployeeOrganizations.Organization" })).ToList();
            return employeesList;
        }

        public async Task<List<Employee>> GetEmployeessForTasksMetbyEmployee(List<int> employeeIds, bool currentPositionsOnly, bool includeTrainees)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();

            predicates.Add(p => employeeIds.Contains(p.Id));

            var employees = (await FindWithIncludeAsync(predicates, new string[] {
                "Person",
                "EmployeePositions.Position.Position_Tasks",
            })).ToList();

            if (currentPositionsOnly)
            {
                foreach (var employee in employees)
                {
                    employee.EmployeePositions = employee.EmployeePositions.Where(ep => ep.Active).ToList();
                }
            }

            if (!includeTrainees)
            {
                foreach (var employee in employees)
                {
                    employee.EmployeePositions = employee.EmployeePositions.Where(ep => !ep.Trainee).ToList();
                }
            }

            return employees;
        }

        public async Task<List<Employee>> GetEmployeesForEmployeeActiveInactiveHistory(List<int> employeeIds)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();

            predicates.Add(p => employeeIds.Contains(p.Id));

            var employees = (await FindWithIncludeAsync(predicates, new string[] {
                "Person",
                "EmployeePositions.Position",
                "EmployeeOrganizations.Organization",
                "EmployeeHistorys"
            })).ToList();

            return employees;
        }

        public async System.Threading.Tasks.Task<Employee> GetEmployeeByClassScheduleEmployeeIdAsync(int cseId)
        {
            var employees = await FindWithIncludeAsync(r => r.ClassSchedule_Employee.Any(cse => cse.Id == cseId), new string[] { "ClassSchedule_Employee", "Person" });
            return employees.FirstOrDefault();
        }

        public async Task<List<Employee>> GetEmployeesForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(List<int> employeeIds)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();

            predicates.Add(p => employeeIds.Contains(p.Id));

            var employees = (await FindWithIncludeAsync(predicates, new string[] {
                "Person",
                "ClassSchedule_Employee.ClassSchedule.ILA",
                "EmployeePositions.Position",
                "EmployeeOrganizations.Organization"
            })).ToList();

            return employees;
        }

        public async Task<List<Employee>> GetEmployeesPersonDetailsByEmpIds(List<int> empIds)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(p => empIds.Contains(p.Id));
            return (await FindWithIncludeAsync(predicates, new string[] { "Person", "EmployeePositions.Position", "EmployeeOrganizations.Organization" }, true)).ToList();
        }

        public async Task<List<Employee>> GetEmployeesForEmployeeTaskQualificationDatesByTaskGenerator(List<int> employeeIds, string activeInactiveAllEmployees, bool includeTrainees)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(p => employeeIds.Contains(p.Id));
            if (activeInactiveAllEmployees == "Active Only")
            {
                predicates.Add(r => r.Active == true);
            }
            else if (activeInactiveAllEmployees == "Inactive Only")
            {
                predicates.Add(r => r.Active == false);
            }
            var employees = (await FindWithIncludeAsync(predicates, new string[] { "EmployeePositions", "Person" }, true)).ToList();
            if (!includeTrainees)
            {
                employees = employees.Where(e => !e.EmployeePositions.Any(ep => ep.Trainee)).ToList();
            }
            return employees;
        }

        public async Task<List<Employee>> GetEmployeesForEmployeeTaskQualificationRecordsForGivenPositionGenerator(List<int> employeeIds, bool includeTrainees)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(p => employeeIds.Contains(p.Id));
            var employees = (await FindWithIncludeAsync(predicates, new string[] { "EmployeePositions", "Person" }, true)).ToList();
            if (!includeTrainees)
            {
                employees = employees.Where(e => !e.EmployeePositions.Any(ep => ep.Trainee)).ToList();
            }
            return employees;
        }

        public async Task<List<Employee>> GetEmployeesWithPersonForTQEvaluatorRoleAsync()
        {
            var employees = await FindWithIncludeAsync(r => r.TQEqulator, new[] { "Person" });
            return employees.ToList();
        }

        public async System.Threading.Tasks.Task<List<Employee>> GetEmployeesAndPositionsByIdAsync(int empId, int positionId)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(p => empId == p.Id);
            var employees = (await FindWithIncludeAsync(predicates, new string[] { "EmployeePositions.Position.Position_Tasks", "Person", "EmployeePositions.Position.TaskListReview_PositionLinks.TaskListReview" }, true)).ToList();
            List<Employee> filteredEmployees = new List<Employee>();
            foreach (var employee in employees)
            {
                var validPositions = employee.EmployeePositions.Where(ep => ep.PositionId == positionId).ToList();

                if (validPositions.Any())
                {
                    employee.EmployeePositions = validPositions;
                    filteredEmployees.Add(employee);
                }
            }
            return filteredEmployees;
        }

        public async Task<List<Employee>> GetEmployeesForSummaryOfTaskQualificationBySubDutyAreaGeneratorAsync(List<int> employeeIds)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(p => employeeIds.Contains(p.Id));
            var employees = (await FindWithIncludeAsync(predicates, new string[] { "Person", "EmployeePositions.Position.Position_Tasks", "EmployeeOrganizations.Organization" }, true)).ToList();
            return employees;
        }

        public async Task<List<Employee>> GetWithCertifications(List<int> employeeIds)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(p => employeeIds.Contains(p.Id));
            var employees = (await FindWithIncludeAsync(predicates, new string[] { "Person", "EmployeeCertifications.Certification.CertifyingBody" }, true)).ToList();

            return employees;
        }

        public async Task<List<Employee>> GetCertifiedEmployeesforGivenCertificateAsync(List<int> employeeIds, string companyEmployeeStatus)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(p => employeeIds.Contains(p.Id));
            if (companyEmployeeStatus == "Active Only")
            {
                predicates.Add(r => r.Active == true);
            }
            else if (companyEmployeeStatus == "Inactive Only")
            {
                predicates.Add(r => r.Active == false);
            }
            var employees = (await FindWithIncludeAsync(predicates, new string[] { "Person", "EmployeePositions.Position", "EmployeeOrganizations.Organization" }, true)).ToList();
            return employees;
        }

        public async Task<Employee> GetEmployeeWithOrgAndPosAsync(int employeeId)
        {
            var employees = (await FindWithIncludeAsync(x => x.Active == true && x.Id == employeeId, new[] { "Person", "EmployeePositions.Position", "EmployeeOrganizations.Organization" })).ToList();
            return employees.FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<Employee>> GetEmployeesByIdsAsync(List<int> employeeIds, bool includeTrainees)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(p => employeeIds.Contains(p.Id));

            var employees = (await FindWithIncludeAsync(predicates, new string[] { "EmployeePositions.Position.Position_Tasks", "Person" }, true)).ToList();

            if (!includeTrainees)
            {
                employees = employees.Select(e =>{e.EmployeePositions = e.EmployeePositions.Where(ep => !ep.Trainee).ToList(); return e;}).Where(e => e.EmployeePositions.Any()).ToList();
            }
            return employees;
        }

        public async System.Threading.Tasks.Task<List<Employee>> GetEmployeesWithPersonPositionCertificationsAsync(List<int> employeeIds)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(p => employeeIds.Contains(p.Id));
            return (await FindWithIncludeAsync(predicates, new string[] { "EmployeePositions","Person", "EmployeeCertifications.Certification" }, true)).ToList();
        }

        public async Task<Employee> GetEmployeeByPersonId(int personId)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(p => p.PersonId == personId);
            return (await FindWithIncludeAsync(predicates, new string[] { "Person"})).FirstOrDefault();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            List<Expression<Func<Employee, bool>>> predicates = new List<Expression<Func<Employee, bool>>>();
            predicates.Add(p => p.Id == id);
            return (await FindWithIncludeAsync(predicates, new string[] { "Person" })).FirstOrDefault();
        }
    }
}
