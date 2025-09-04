using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ProcedureReview;
using QTD2.Infrastructure.Model.ProcedureReview_Employee;
using QTD2.Infrastructure.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IProcedureReviewService
    {
        public Task<Result<ProcedureReview>> CreateAsync(CreateProcedureReviewDto options);
        public Task<Result> UpdateAsync(int id, CreateProcedureReviewDto options);
        public Task<List<ProcedureReviewOverviewVM>> GetListAsync();
        public Task<Result<ProcedureReview>> GetAsync(int id);
        
        public System.Threading.Tasks.Task DeleteAsync(ProcedureReviewOptions options);

        public System.Threading.Tasks.Task DeactivateAsync(ProcedureReviewOptions options);

        public System.Threading.Tasks.Task ActivateAsync(ProcedureReviewOptions options);

        public Task<ProcedureReview> LinkEmployee(int procedureReviewId, ProcedureReview_EmployeeCreateOptions options);

        public System.Threading.Tasks.Task UnlinkEmployee(int procedureReviewId, int[] empIDs);
        public Task<List<ProcedureReview>> GetProcedureReviewEmployeesLinkedTo(int id);

        public Task<List<EmployeesLinkedToProcedureReview>> GetLinkedEmployees(int id);

        public Task<ProcedureReview> PublishProcedureReview(int id);

        public Task<ProcedureReviewVM> GetStatsCount();

        public Task<ProcedureReview_Employee> UpdateResponseAsync(int empId, ProcedureReviewResponseCreateOptions options);

        public Task<ProcedureReview_Employee> UpdateCommentsAsync(int empId, ProcedureReviewResponseCreateOptions options);

        public Task<List<ProcedureReview>> GetDraftsProcedureReviews();

        public Task<List<ProcedureReview_Employee>> GetEmployeesPendingProcedureReviews();

        public Task<List<ProcedureReview>> GetPublishedList();



    }
}
