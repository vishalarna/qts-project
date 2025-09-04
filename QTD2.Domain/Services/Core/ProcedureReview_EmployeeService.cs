using Microsoft.EntityFrameworkCore;
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
    public class ProcedureReview_EmployeeService : Common.Service<ProcedureReview_Employee>, IProcedureReview_EmployeeService
    {
        public ProcedureReview_EmployeeService(IProcedureReview_EmployeeRepository repository, IProcedureReview_EmployeeValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ProcedureReview_Employee>> GetEmpProcedureReviewByIdAsync(int employeeId)
        {
            var currentDate = DateTime.UtcNow;
            var obj_list = await FindWithIncludeAsync(x => x.EmployeeId == employeeId && x.ProcedureReview.IsPublished && x.ProcedureReview.Active && x.ProcedureReview.StartDateTime < currentDate, new string[] { "ProcedureReview.Procedure", "Employee" });
            return obj_list.ToList();
        }

        public async Task<ProcedureReview_Employee> GetForNotificationAsync(int procedureReviewEmployeeId)
        {
            var obj_list = await FindWithIncludeAsync(x => x.Id == procedureReviewEmployeeId && x.IsCompleted==false, new string[] { "ProcedureReview.Procedure", "Employee.Person" });
            return obj_list.First();
        }

        public async Task<List<ProcedureReview_Employee>> GetProcedureReviewByIdAsync(int employeeId)
        {
            var procedureReviews = await FindAsync(x => x.EmployeeId == employeeId, true);
            return procedureReviews.ToList();
        }

        public async Task<List<ProcedureReview_Employee>> GetDashboardProcedureReviewsAsync(int employeeId)
        {
            List<Expression<Func<ProcedureReview_Employee, bool>>> predicates = new List<Expression<Func<ProcedureReview_Employee, bool>>>();
            if (employeeId > 0)
            {
                predicates.Add(r => r.EmployeeId == employeeId && r.ProcedureReview.StartDateTime < DateTime.Today);
            }
            predicates.Add(r => r.ProcedureReview.IsPublished == true && r.ProcedureReview.EndDateTime > DateTime.Today);
            var result = await FindWithIncludeAsync(predicates, new string[] { "ProcedureReview" });
            return result.ToList();
        }

        public async Task<List<ProcedureReview_Employee>> GetAllProcedureReview_EmployeesAsync()
        {
            return (await AllAsync()).ToList();
        }

		public async Task<List<ProcedureReview_Employee>> GetForProcedureReviewCompletionHistorybyEmployee(List<int> employeeIds, string publishedProcedureReviews, List<DateTime> completionDateRange, string completionStatus)
		{
            List<Expression<Func<ProcedureReview_Employee, bool>>> predicates = new List<Expression<Func<ProcedureReview_Employee, bool>>>();
            predicates.Add(r => employeeIds.Contains(r.EmployeeId));
            if (publishedProcedureReviews == "Published")
            {
                predicates.Add(r => r.ProcedureReview.IsPublished);
            }
            else if (publishedProcedureReviews == "Draft")
            {
                predicates.Add(r => !r.ProcedureReview.IsPublished);
            }

            if (completionDateRange.Count == 2)
            {
                predicates.Add(r => (r.CompletedDate.HasValue && r.CompletedDate >= completionDateRange[0] && r.CompletedDate <= completionDateRange[1]) || !r.CompletedDate.HasValue);
            }

            var result = (await FindWithIncludeAsync(predicates
                , new string[] {
                    "ProcedureReview.Procedure.Procedure_IssuingAuthority",
                    "Employee.Person",
                    "Employee.EmployeePositions.Position",
                    "Employee.EmployeeOrganizations.Organization"
                })).ToList();

            if (completionStatus != "All")
            {
                result = result.Where(x => x.getStatus() == completionStatus).ToList();
            }

            return result;
        }

        public async Task<List<ProcedureReview_Employee>> GetCurrentProcedureReviewsForEmployee(int employeeId)
        {
            var currentDate = DateTime.UtcNow;

            var procedureReviewEmployees = await FindAsync(r => !r.ProcedureReview.Procedure.Deleted && r.EmployeeId == employeeId && r.ProcedureReview.IsPublished && r.ProcedureReview.Active && r.ProcedureReview.StartDateTime < currentDate);

            return procedureReviewEmployees.ToList();
        }
    }
}
