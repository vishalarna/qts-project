using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IProcedureReview_EmployeeService : Common.IService<ProcedureReview_Employee>
    {
        public Task<List<ProcedureReview_Employee>> GetEmpProcedureReviewByIdAsync(int employeeId);

        public Task<List<ProcedureReview_Employee>> GetProcedureReviewByIdAsync(int employeeId);
        System.Threading.Tasks.Task<ProcedureReview_Employee> GetForNotificationAsync(int procedureReviewEmployeeId);
        public Task<List<ProcedureReview_Employee>> GetAllProcedureReview_EmployeesAsync();
        public Task<List<ProcedureReview_Employee>> GetForProcedureReviewCompletionHistorybyEmployee(List<int> employeeIds, string publishedProcedureReviews, List<DateTime> completionDateRange, string completionStatus);
        Task<List<ProcedureReview_Employee>> GetCurrentProcedureReviewsForEmployee(int employeeId);
    }
}
