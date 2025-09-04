using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;

namespace QTD2.Domain.Services.Core
{
    public class ClassScheduleEmployeeService : Common.Service<ClassSchedule_Employee>, IClassScheduleEmployeeService
    {
        public ClassScheduleEmployeeService(IClassScheduleEmployeeRepository repository, IClassSchedule_EmployeeValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<ClassSchedule_Employee> GetEmployeeForClassScheduleAsync(int classScheduleId, int employeeId)
        {
            var classScheduleEmployee = await FindWithIncludeAsync(r => r.ClassScheduleId == classScheduleId && r.EmployeeId == employeeId, new[] { "ScormRegistrations", "ClassSchedule", "Employee.Person" });
            return classScheduleEmployee.FirstOrDefault();
        }
        public async Task<List<ClassSchedule_Employee>> GetEmployeeSelfRegistrationDeniedCourse(int employeeId)
        {
            var classScheduleEmployees = await FindWithIncludeAsync(x => x.EmployeeId == employeeId && x.IsDenied == true, new string[] { "ClassSchedule", "ClassSchedule.Instructor", "ClassSchedule.Location", "ClassSchedule.ILA.Provider" });
            return classScheduleEmployees.ToList();
        }

        public async Task<List<ClassSchedule_Employee>> GetEmployeeSelfRegistrationApprovedCourse(int employeeId)
        {
            var classScheduleEmployees = await FindWithIncludeAsync(x => x.EmployeeId == employeeId && x.IsEnrolled == true && x.IsWaitlisted != true && x.IsDenied != true && x.IsDropped != true, new string[] { "ClassSchedule", "ClassSchedule.Instructor", "ClassSchedule.Location", "ClassSchedule.ILA.Provider" });
            return classScheduleEmployees.ToList();
        }

        public async Task<List<ClassSchedule_Employee>> GetEmployeeSelfRegistrationDroppedCourse(int employeeId)
        {
            var classScheduleEmployees = await FindWithIncludeAsync(x => x.EmployeeId == employeeId && x.IsDropped == true && x.IsEnrolled != true && x.IsDenied != true, new string[] { "ClassSchedule", "ClassSchedule.Instructor", "ClassSchedule.Location", "ClassSchedule.ILA.Provider", "ClassSchedule.ClassSchedule_SelfRegistrationOption" });
            return classScheduleEmployees.ToList();
        }

        public async Task<int> GetEmployeeByClassScheduleByIdAsync(int employeeId, int ClassScheduleId)
        {
            var registeredStudents = await GetCount(x => x.EmployeeId != employeeId && x.IsEnrolled == true && x.ClassScheduleId == ClassScheduleId);
            return registeredStudents;
        }

        public async Task<List<ClassSchedule_Employee>> GetClassScheduleEmployeeByIdAndInclude(int empId, string[] includes)
        {
            var csEmps = await FindQueryWithIncludeAsync(x => x.EmployeeId == empId, includes, false).ToListAsync();
            return csEmps;
        }

        public async Task<List<ClassSchedule_Employee>> GetCSEmpsWithConditionAndIncludes(Expression<System.Func<ClassSchedule_Employee, bool>> predicate, string[] includes)
        {
            if (includes != null && includes.Length > 0)
            {
                var csEmps = await FindQueryWithIncludeAsync(predicate, includes).ToListAsync();
                return csEmps;
            }
            else
            {
                var csEmps = await FindAsync(predicate);
                return csEmps.ToList();
            }
        }

        public async Task<List<ClassSchedule_Employee>> GetCompactCSEmpsWithConditionAndIncludes(Expression<System.Func<ClassSchedule_Employee, bool>> predicate, string[] includes)
        {
            if (includes != null && includes.Length > 0)
            {
                var csEmps = await FindQueryWithIncludeAsync(predicate, includes).Select(s => new ClassSchedule_Employee
                {
                    Id = s.Id,
                    Active = s.Active,
                    EmployeeId = s.EmployeeId
                }).ToListAsync();
                return csEmps;
            }
            else
            {
                var csEmps = await FindAsync(predicate);
                return csEmps.Select(s => new ClassSchedule_Employee
                {
                    Id = s.Id,
                    Active = s.Active,
                    EmployeeId = s.EmployeeId
                }).ToList();
            }
        }

        public async Task<ClassSchedule_Employee> GetForNotificationAsync(int classScheduleEmployeeId)
        {
            var classScheduleEmployee = await FindWithIncludeAsync(x => classScheduleEmployeeId == x.Id,
                new string[] {
                    "ClassSchedule",
                    "ClassSchedule.Instructor",
                    "ClassSchedule.Location",
                    "ClassSchedule.ILA.Provider",
                    "ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest",
                    "ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest",
                    "Employee.Person"
                });

            return classScheduleEmployee.FirstOrDefault();
        }

        public async Task<List<ClassSchedule_Employee>> GetCompletedOnlineCoursesForEmployeeAsync(int employeeId)
        {
            var now = System.DateTime.Now;

            var classScheduleEmployees = await FindWithIncludeAsync(
                    r => r.EmployeeId == employeeId
                    && r.CompletionDate.HasValue
                    && r.ClassSchedule.StartDateTime.AddDays(365) > now,
                    new[] { "ScormRegistrations", "ClassSchedule.ILA" });
            return classScheduleEmployees.ToList();
        }

        public async Task<List<ClassSchedule_Employee>> GetPendingOnlineCoursesForEmployeeAsync(int employeeId)
        {
            var classScheduleEmployees = await FindWithIncludeAsync(i => i.EmployeeId == employeeId && !i.CompletionDate.HasValue && i.ScormRegistrations.Any(r => r.RegistrationCompletion != CBT_ScormRegistrationCompletion.COMPLETED && r.Active) && i.IsEnrolled, new[] { "ScormRegistrations", "ClassSchedule.ClassSchedule_TestReleaseEMPSettings.PreTest", "ClassSchedule.ClassSchedule_TestReleaseEMPSettings.FinalTest", "Employee", "ClassSchedule.ILA.Provider" });
            return classScheduleEmployees.ToList();
        }
        public async System.Threading.Tasks.Task<List<ClassSchedule_Employee>> GetForCEHExportAsync(int classScheduleId)
        { 
            var classScheduleEmployees = (await FindWithIncludeAsync(r => r.ClassSchedule.Id == classScheduleId && r.IsEnrolled, new[] { "ClassSchedule.ILA.ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement", "Employee.EmployeeCertifications.Certification.CertifyingBody" })).ToList();
            return classScheduleEmployees;
        }

        public async Task<ClassSchedule_Employee> GetByEmployeeIdAndClassScheduleIdAsync(int employeeId, int classScheduleId)
        {
            return (await FindAsync(r => r.EmployeeId == employeeId && r.ClassScheduleId == classScheduleId)).FirstOrDefault();
        }

        public async Task<List<ClassSchedule_Employee>> GetClassSchedulesForIDP(int iLAId, System.DateTime iDPYear, int employeeId)
        {
            return (await FindWithIncludeAsync(r => r.EmployeeId == employeeId && r.ClassSchedule.ILAID == iLAId && (iDPYear.Year == r.ClassSchedule.StartDateTime.Year || iDPYear.Year == r.ClassSchedule.EndDateTime.Year), new string[] { "ClassSchedule" })).ToList();
        }

        public async Task<string> GetEmployeeNameByIdAsync(int classScheduleEmployeeId)
        {
            var classScheduleEmployee = await GetWithIncludeAsync(classScheduleEmployeeId, new[] { "Employee.Person" });
            return classScheduleEmployee?.Employee?.Person != null ? classScheduleEmployee.Employee.Person.FirstName + " " + classScheduleEmployee.Employee.Person.LastName : null;
        }

        public async Task<List<ClassSchedule_Employee>> GetWaitlistedAsync()
        {
            var classScheduleEmployees = await FindWithIncludeAsync(r => r.IsWaitlisted, new[] { "ClassSchedule.ILA", "Employee.Person", "ClassSchedule.ClassSchedule_SelfRegistrationOption", "ClassSchedule.ClassSchedule_Employee" });
            return classScheduleEmployees.ToList();
        }

        public async Task<List<ClassSchedule_Employee>> GetClassScheduleEmployeesReadyForRetakeAsync()
        {
            var now = System.DateTime.UtcNow;

            var classScheduleEmployees = await FindWithIncludeAsync(r =>
                                                r.ClassSchedule.ClassSchedule_TestReleaseEMPSettings != null
                                                && r.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.RetakeEnabled
                                                && r.IsEnrolled
                                                && r.ClassSchedule.StartDateTime < now,
                                                //&& !r.IsComplete
                                                new string[] {
                                                        "Employee.Person",
                                                        "ClassSchedule.Location",
                                                        "ClassSchedule.Instructor",
                                                        "ClassSchedule.ClassSchedule_SelfRegistrationOption",
                                                        "ClassSchedule.ILA",
                                                        "ClassSchedule.ClassSchedule_TestReleaseEMPSettings.ClassSchedule_TestReleaseEMPSetting_RetakeLinks.RetakeTest"
                                                });

            classScheduleEmployees = classScheduleEmployees.Where(r => now < (r.ClassSchedule.ClassSchedule_TestReleaseEMPSettings == null ? r.ClassSchedule.EndDateTime : r.ClassSchedule.ClassSchedule_TestReleaseEMPSettings.GetDueDate(r.ClassSchedule.EndDateTime)));
            return classScheduleEmployees.ToList();
        }

        public async Task<List<ClassSchedule_Employee>> GetForCertificationCalculationAsync(List<int> employeeIds)
        {
            var classScheduleEmployees = (await FindWithIncludeAsync(r =>
            r.Active &&
            r.IsEnrolled &&
            employeeIds.Contains(r.EmployeeId), new[] { "ClassSchedule.ILA.ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement", "ClassSchedule.ILA.Provider" })).ToList();
            return classScheduleEmployees;
        }

        public async Task<List<ClassSchedule_Employee>> GetClassScheduleEmployeeByCBT(CBT cbt)
        {
            var currentDate = DateTime.Now.Date;
            List<Expression<Func<ClassSchedule_Employee, bool>>> predicates = new List<Expression<Func<ClassSchedule_Employee, bool>>>();
            predicates.Add(r => r.ClassSchedule.ILAID == cbt.ILAId);
            predicates.Add(r => r.Active);
            predicates.Add(r => r.CBTStatusId != 3);

            var classScheduleEmployees = (await FindWithIncludeAsync(predicates, new[] { "ClassSchedule.ILA" })).ToList();

            var filteredResults = classScheduleEmployees.Where(r =>
            {
                DateTime? adjustedEndDate = cbt?.GetDueDate(r.ClassSchedule.EndDateTime);
                return adjustedEndDate > currentDate;
            }).ToList();

            return filteredResults;
        }

        public async Task<List<ClassSchedule_Employee>> GetForNercCertificationCalculationAsync(List<int> employeeIds)
        {
            List<Expression<Func<ClassSchedule_Employee, bool>>> predicates = new List<Expression<Func<ClassSchedule_Employee, bool>>>();
            predicates.Add(r => r.Active);
            predicates.Add(r => r.IsEnrolled);
            predicates.Add(r => employeeIds.Contains(r.EmployeeId));
            predicates.Add(r => r.ClassSchedule.ILA.Provider.IsNERC);
            var classScheduleEmployees = (await FindWithIncludeAsync(predicates,
                new[] { "ClassSchedule.ILA.ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement"},true)).ToList();

            var classScheduleEmpILACertificationPartialCredits = (await FindWithIncludeAsync(predicates, new[] { "ClassScheduleEmployee_ILACertificationLink_PartialCredits.ClassScheduleEmployee_ILACertificationLink_SubRequirement_PartialCredits" }, true)).SelectMany(p=>p.ClassScheduleEmployee_ILACertificationLink_PartialCredits).ToList();

            var providers = (await FindWithIncludeAsync(predicates,
                new[] { "ClassSchedule.ILA.Provider" },true)).Select(r => r.ClassSchedule.ILA.Provider).Distinct().ToList();

            classScheduleEmployees.ForEach(r => r.ClassSchedule.ILA.Provider = providers.Where(s => s.Id == r.ClassSchedule.ILA.ProviderId).FirstOrDefault());
            classScheduleEmployees.ForEach(r => r.ClassScheduleEmployee_ILACertificationLink_PartialCredits = classScheduleEmpILACertificationPartialCredits.Where(m => m.ClassScheduleEmployeeId == r.Id).ToList());

            return classScheduleEmployees;
        }

        public async Task<List<ClassSchedule_Employee>> GetForEmergencyResponseCertificationCalculationAsync(List<int> employeeIds, List<int> certificationIds)
        {
            var classScheduleEmployees = (await FindWithIncludeAsync(r =>
                   r.Active &&
                   r.IsEnrolled &&
                   employeeIds.Contains(r.EmployeeId) &&
                   r.ClassSchedule.ILA.ILACertificationLinks.Any(icl => certificationIds.Contains(icl.CertificationId)),
                new[] { "ClassSchedule.ILA.ILACertificationLinks.ILACertificationSubRequirementLink.CertificationSubRequirement", "ClassSchedule.ILA.Provider" })).ToList();
            return classScheduleEmployees;
        }

        public async Task<List<ClassSchedule_Employee>> GetClassSchdulesEmployeeByEmployeeIdAsync(List<int> employeeIds)
        {
            var classScheduleEmployees = (await FindAsync(r => employeeIds.Contains(r.EmployeeId))).ToList();
            return classScheduleEmployees;
        }

        public async Task<List<ClassSchedule_Employee>> GetClassCertificatesAsync(List<int> classScheduleIds, bool printForThoseWithNoGradeAwarded, bool includeFailedStudents)
        {
            List<string> failedGrades = new List<string>() { "F", "W", "O" };
            List<Expression<Func<ClassSchedule_Employee, bool>>> predicates = new List<Expression<Func<ClassSchedule_Employee, bool>>>();

            predicates.Add(r => classScheduleIds.Contains(r.ClassScheduleId));

            if (!printForThoseWithNoGradeAwarded)
            {
                predicates.Add(r => !String.IsNullOrEmpty(r.FinalGrade));
            }

            if (!includeFailedStudents)
            {
                predicates.Add(r => !failedGrades.Contains(r.FinalGrade));
            }

            var classScheduleEmployees = await FindWithIncludeAsync(predicates,
                new string[] {
                    "ClassSchedule.Instructor",
                    "ClassSchedule.Location",
                    "Employee.Person"
                }, true);

            return classScheduleEmployees.ToList();
        }

        public async Task<List<ClassSchedule>> GetClassSchedulesForILAsAndEmployeeAsync(List<int>ilaIds, int employeeId)
        {
            return (await FindWithIncludeAsync(x => ilaIds.Contains(x.ClassSchedule.ILAID.Value) && x.EmployeeId == employeeId, new[] { "ClassSchedule" })).Select(e => e.ClassSchedule).ToList();
        }
    }
}
