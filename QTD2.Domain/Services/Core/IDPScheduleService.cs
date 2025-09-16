using QTD2.Domain.Entities.Core;
using QTD2.Domain.Interfaces.Repository.Core;
using QTD2.Domain.Interfaces.Service.Core;
using QTD2.Domain.Interfaces.Validation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Services.Core
{
    public class IDPScheduleService : Common.Service<IDPSchedule>, IIDPScheduleService
    {

        public IDPScheduleService(IIDPScheduleRepository repository, IIDPScheduleValidation validation)
           : base(repository, validation)
        {
        }

        public async System.Threading.Tasks.Task<List<IDPSchedule>> GetAllIDPAsync(DateTime startDate, DateTime endDate)
        {
            List<Expression<Func<IDPSchedule, bool>>> predicates = new List<Expression<Func<IDPSchedule, bool>>>();
            predicates.Add(r => r.startDate >= startDate && r.endDate <= endDate);

            var idpSchdules = await FindWithIncludeAsync(predicates, new string[] { "IDP.ILA", "IDP.Employee.Person", "IDP.Employee.EmployeeCertifications", "ClassSchedule.ClassSchedule_Employee", "ClassSchedule.Instructor", "ClassSchedule.Location" });
            return idpSchdules.ToList();
        }

        public async Task<List<IDPSchedule>> GetForTrainingProgramCompletionReportAsync(List<int> trainingProgramId, List<DateTime> dateRanges, bool includeInActiveIla, string activeInactiveEmployees)
        {
            List<Expression<Func<IDPSchedule, bool>>> predicates = new List<Expression<Func<IDPSchedule, bool>>>();
            if (dateRanges.Count() > 0)
            {
                predicates.Add(r => r.endDate >= dateRanges[0] && r.endDate <= dateRanges[1]);
                predicates.Add(r => r.startDate >= dateRanges[0] && r.startDate <= dateRanges[1]);
            }

            if (activeInactiveEmployees == "Active Only")
            {
                predicates.Add(i => i.IDP.Employee.Active);
            }
            else if (activeInactiveEmployees == "Inactive Only")
            {
                predicates.Add(i => !i.IDP.Employee.Active);
            }

            if (!includeInActiveIla)
            {
                predicates.Add(r => r.IDP.ILA.Active);
            }

            var idpSchdules = await FindWithIncludeAsync(predicates,
                new string[] { "IDP.ILA"},true);

            return idpSchdules.ToList();
        }

		public async Task<List<IDPSchedule>> GetIDPSchedulesForEmployeeCourseScheduleforGivenYear(List<int> employeeIds, string year, string activeInactiveAllILAs, string ilaCompletionStatus)
		{
            List<Expression<Func<IDPSchedule, bool>>> predicates = new List<Expression<Func<IDPSchedule, bool>>>();

            predicates.Add(i => employeeIds.Contains(i.IDP.EmployeeId));

            predicates.Add(i => i.IDP.IDPYear.HasValue && i.IDP.IDPYear.Value.Year.ToString() == year);

            if (activeInactiveAllILAs == "Active Only") 
            {
                predicates.Add(i => i.IDP.ILA.Active);
            }
            else if (activeInactiveAllILAs == "Inactive Only") 
            {
                predicates.Add(i => !i.IDP.ILA.Active);
            }
            predicates.Add(i => !i.Deleted);

            var idpSchedules = (await FindWithIncludeAsync(predicates, new string[] {
                "IDP.Employee.Person",
                "IDP.Employee.EmployeePositions.Position",
                "IDP.Employee.EmployeeOrganizations.Organization",
                "IDP.ILA.DeliveryMethod",
                "ClassSchedule.ClassSchedule_Employee"
            })).ToList();

            if (ilaCompletionStatus == "Completed") {
                foreach (var idpSchedule in idpSchedules)
                {
                    idpSchedule.ClassSchedule.ClassSchedule_Employee = idpSchedule.ClassSchedule.ClassSchedule_Employee.Where(cse => cse.IsComplete).ToList();
                }
            } else if (ilaCompletionStatus == "Not Completed"){
                foreach (var idpSchedule in idpSchedules)
                {
                    idpSchedule.ClassSchedule.ClassSchedule_Employee = idpSchedule.ClassSchedule.ClassSchedule_Employee.Where(cse => !cse.IsComplete).ToList();
                }
            }

            return idpSchedules;
        }

        public async Task<List<IDPSchedule>> GetIDPSchedulesForEmployeeIDPCompletionStatusReportFulfillments(int year, List<int> employeeIds)
        {
            List<Expression<Func<IDPSchedule, bool>>> predicates = new List<Expression<Func<IDPSchedule, bool>>>();

            predicates.Add(p => p.IDP.IDPYear.HasValue && p.IDP.IDPYear.Value.Year == year);

            var idpSchedules = (await FindWithIncludeAsync(predicates, new[] { "IDP.ILA", "ClassSchedule.ClassSchedule_Employee" } )).ToList();

            return idpSchedules.Where(idp => employeeIds.Contains(idp.IDP.EmployeeId) && !idp.Deleted).ToList();
        }

        public async System.Threading.Tasks.Task<IDPSchedule> GetIDPSchedulesByClassIdAndEmpIdAsync(int classId, int empId)
        {
            return (await FindAsync(x => x.ClassScheduleId == classId && x.IDP.EmployeeId == empId)).FirstOrDefault();
        }

        public async System.Threading.Tasks.Task<List<IDPSchedule>> GetIDPSchedulesByClassScheduleIdAsync(int classId)
        {
            var idpschedules = await FindAsync(x => x.ClassScheduleId == classId);
            return idpschedules.ToList();
        }
    }
}
