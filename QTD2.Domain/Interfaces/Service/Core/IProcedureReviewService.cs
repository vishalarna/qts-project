using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IProcedureReviewService : Common.IService<ProcedureReview>
    {
        System.Threading.Tasks.Task<List<ProcedureReview>> GetForNotificationCatchupAsync();
        System.Threading.Tasks.Task<ProcedureReview> GetWithEmployeesAsync(int procedureReviewId);
        System.Threading.Tasks.Task<List<ProcedureReview>> GetAllWithProcedureAndProcedureEmployee();
        System.Threading.Tasks.Task<List<ProcedureReview>> GetProcedureReviewsByIdAsync(List<int> procedureReviewIds);
    }
}
