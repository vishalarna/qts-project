using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ProcedureReview;
using QTD2.Infrastructure.Model.ProcedureReviewEmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IProcedureReviewEmpService
    {
        public Task<List<ProcedureReviewEmpModel>> GetEmpProcedureReviewsAsync();

        public Task<ProcedureReviewEmpModel> GetEmpProcedureReviewDataByIdAsync(int procedureReviewId);

        public Task<ProcedureReview_Employee> SubmitProcedureReviewAsync(SubmitProcedureReviewDto submitOptions);

        public Task<ProcedureReview_Employee> CancelProcedureReviewAsync(int procedureReviewId, string response, string comments);

        //New Application Services
        public Task<List<ProcedureReviewEmpModel>> GetEmpProcedureReviewsByIdAsync(int employeeId);
    }
}
