using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.ILA_Topic;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.Procedure_EnablingObjective_Link;
using QTD2.Infrastructure.Model.Procedure_ILA_Link;
using QTD2.Infrastructure.Model.Procedure_IssuingAuthority;
using QTD2.Infrastructure.Model.Procedure_RegRequirement_Link;
using QTD2.Infrastructure.Model.Procedure_SaftyHazard_Link;
using QTD2.Infrastructure.Model.Procedure_StatusHistory;
using QTD2.Infrastructure.Model.Procedure_Task_Link;
using QTD2.Infrastructure.Model.Provider;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.Task;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IProcedureService
    {
        public Task<List<Procedure>> GetAsync();

        public Task<Procedure> GetAsync(int id);

        public Task<Procedure> GetOnlyProc(int id);

        public Task<Procedure> CreateAsync(ProcedureCreateOptions options);

        public Task<Procedure> CopyProcedureWithLinkages(int id, ProcedureCreateOptions options);

        public Task<Procedure> UpdateAsync(int id, ProcedureUpdateOptions options);

        public System.Threading.Tasks.Task DeactivateAsync(int id, ProcedureOptions options);

        public System.Threading.Tasks.Task ActivateAsync(int id, ProcedureOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id, ProcedureOptions options);

        public Task<Procedure> LinkEnablingObjectiveAsync(int procedureId, Procedure_EnablingObjective_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkEnablingObjectiveAsync(int procedureId, int[] enablingObjectiveId);

        public Task<Procedure> LinkSaftyHazardAsync(int procedureId, Procedure_SaftyHazard_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkSaftyHazardAsync(int procedureId, Procedure_SaftyHazard_LinkOptions options);

        public Task<List<SafetyHazardWithLinkCount>> GetLinkedSaftyHazardsAsync(int id);

        public Task<Procedure> LinkTask(int procId, Procedure_Task_LinkCreateOptions options);

        public Task<Procedure> LinkILA(int procId, Procedure_ILA_LinkCreateOptions options);

        public Task<List<EnablingObjectiveWithCountOptions>> GetLinkedEnablingObjectivesAsync(int id);

        public System.Threading.Tasks.Task UnlinkTask(int procId, int[] taskId);

        public System.Threading.Tasks.Task UnlinkILA(int procId, int[] iLAId);

        public Task<List<TaskWithCountOptions>> GetLinkedTasks(int id);

        public Task<List<ILAWithCountOptions>> GetLinkedILAs(int id);

        public Task<List<Procedure_IssuingAuthority>> GetProcedure_IssuingAuthoritiesAsync();

        public Task<Procedure_IssuingAuthority> GetProcedure_IssuingAuthorityAsync(int id);

        public System.Threading.Tasks.Task ActiveProcedure_IssuingAuthorityAsync(int id);

        public System.Threading.Tasks.Task InActiveProcedure_IssuingAuthorityAsync(int id);

        public System.Threading.Tasks.Task DeleteProcedure_IssuingAuthorityAsync(int id, Procedure_IssuingAuthorityOptions options);

        public Task<Procedure_IssuingAuthority> UpdateProcedure_IssuingAuthorityAsync(int id, Procedure_IssuingAuthorityCreateOptions options);

        public Task<Procedure_IssuingAuthority> CreateProcedure_IssuingAuthorityAsync(Procedure_IssuingAuthorityCreateOptions options);

        public System.Threading.Tasks.Task StoreStatusHistoryAsync(Procedure_StatusHistoryCreateOptions options);

        public Task<List<ProcedureLatestActivityVM>> GetHistoryAsync(bool getLatest);

        public Task<Procedure> LinkRR(int id, Procedure_RR_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkRR(int procId, Procedure_RR_LinkOptions options);

        public Task<ProcedureStatsVM> GetStatsCount();

        public Task<List<IssueAuthorityTreeVM>> GetNotLinked(string notLinkedWith);

        public Task<List<RegulatoryRequirementWithLinkCount>> GetLinkedRR(int id);

        public Task<List<Procedure>> GetSHLinkedProcedures(int id);

        public Task<List<Procedure>> GetEOLinkedProcedures(int id);

        public Task<List<Procedure>> GetProcTaskIsLinkedTo(int id);

        public Task<List<Procedure>> GetProcILAIsLinkedTo(int id);

        public Task<List<Procedure>> GetProceduresRRIsLinkedTo(int id);

        public Task<List<ILAProviderDataVM>> GetProviderWithILAs();

        public Task<List<ILATopicDataVM>> GetTopicWithILAs();

        public Task<bool> IsIssuingAuthorityReleasedToEmp(int issuingAuthorityId);
        public Task<bool> IsProcedureReleasedToEmp(int procedureId);

        //active inactive procedure and ia
        public Task<List<ProcedureSummaryVM>> GetActiveInactiveIA(string notLinkedWith);

        public Task<List<ProcedureSummaryVM>> GetActiveInactiveProcedure(string notLinkedWith);


    }
}
