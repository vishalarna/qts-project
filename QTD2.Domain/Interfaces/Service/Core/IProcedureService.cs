using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QTD2.Domain.Interfaces.Service.Core
{
    public interface IProcedureService : Common.IService<Entities.Core.Procedure>
    {
        System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Procedure>> GetAllProceduresByIssuingAuthorityAsync();
        public System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Procedure>> GetAllProcedureCompletionHistoryAsync(string employeeStatus, string completedStatus, DateTime startDate, DateTime endDate, List<int> procedureIds, List<int> positionIds, List<int> organizationIds);
        public System.Threading.Tasks.Task<List<QTD2.Domain.Entities.Core.Procedure>> GetProceduresForEmployeeTrainingTowardProceduresAndRegulatoryRequirements(List<int> procedureIds);
        Task<List<Procedure>> GetAllProceduresByIssuingAuthoritiesAsync(List<int> issuingAuthorityIds, bool includeInactive);
        Task<List<Procedure>> GetProceduresByIDAsync(List<int> procedureIds);
        Task<Procedure> GetProceduresByIDAndNumberAsync(int procedureId, string procedureNumber);
    }
}
