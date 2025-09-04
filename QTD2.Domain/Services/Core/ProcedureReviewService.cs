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
    public class ProcedureReviewService : Common.Service<ProcedureReview>, IProcedureReviewService
    {
        public ProcedureReviewService(IProcedureReviewRepository repository, IProcedureReviewValidation validation)
            : base(repository, validation)
        {
        }

        public async Task<List<ProcedureReview>> GetForNotificationCatchupAsync()
        {
            var prs = await FindWithIncludeAsync(r => r.StartDateTime < DateTime.UtcNow && r.EndDateTime > DateTime.UtcNow, new string[] { "ProcedureReview_Employee.Employee" });
            return prs.ToList();
        }

        public async Task<ProcedureReview> GetWithEmployeesAsync(int procedureReviewId)
        {
            var pr = await FindWithIncludeAsync(r => r.Id == procedureReviewId, new string[] { "ProcedureReview_Employee.Employee.Person" });
            return pr.FirstOrDefault();
        }

        public async Task<List<ProcedureReview>> GetAllWithProcedureAndProcedureEmployee()
        {
            // var pr = await AllWithIncludeAsync(new string[] { "ProcedureReview_Employee", "Procedure.Procedure_IssuingAuthority" });

            var pr = FindQuery(x => true, true).Select(x => new ProcedureReview
            {
                ProcedureReview_Employee = x.ProcedureReview_Employee,
                Id = x.Id,
                StartDateTime = x.StartDateTime,
                EndDateTime = x.EndDateTime,
                IsPublished = x.IsPublished,
                Active = x.Active,
                ProcedureReviewTitle = x.ProcedureReviewTitle,
                Procedure = new Procedure()
                {
                    Procedure_IssuingAuthority = x.Procedure.Procedure_IssuingAuthority,
                    Number = x.Procedure.Number,
                    Title = x.Procedure.Title,
                    IssuingAuthorityId = x.Procedure.IssuingAuthorityId
                }
            });
            return pr.ToList();
        }

        public async Task<List<ProcedureReview>> GetProcedureReviewsByIdAsync(List<int> procedureReviewIds)
        {
            var prs = await FindWithIncludeAsync(r => procedureReviewIds.Contains(r.Id), new string[] { "Procedure" });
            return prs.ToList();
        }
    }
}
