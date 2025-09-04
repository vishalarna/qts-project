using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.ILA_Topic;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.Provider;
using QTD2.Infrastructure.Model.RegRequirement_EO_Link;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.RR_EO_Link;
using QTD2.Infrastructure.Model.RR_ILA_Link;
using QTD2.Infrastructure.Model.RR_Procedure_Link;
using QTD2.Infrastructure.Model.RR_SafetyHazard_Link;
using QTD2.Infrastructure.Model.RR_Task_Link;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.Task;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IRegulatoryRequirementService
    {
        public Task<List<RegulatoryRequirement>> GetAsync();

        public Task<RegulatoryRequirement> GetAsync(int id);

        public Task<RegulatoryRequirement> CreateAsync(RegulatoryRequirementCreateOptions options);

        public Task<RegulatoryRequirement> CopyRRWithLinkages(int id, RegulatoryRequirementCreateOptions options);

        public Task<RegulatoryRequirement> UpdateAsync(int id, RegulatoryRequirementCreateOptions options);

        public System.Threading.Tasks.Task DeleteAsync(int id, RegulatoryRequirementOptions options);

        public System.Threading.Tasks.Task ActiveAsync(int id, RegulatoryRequirementOptions options);

        public System.Threading.Tasks.Task InActiveAsync(int id, RegulatoryRequirementOptions options);

        public Task<RegulatoryRequirementCompact> GetRegulatoryRequirementCompactDataAsync(int id);

        public Task<RegulatoryRequirement> LinkSafetyHazardAsync(int id, RR_SafetyHazard_LinkOptions safetyHazardId);

        public System.Threading.Tasks.Task UnlinkSafetyHazardAsync(int id, RR_SafetyHazard_LinkOptions safetyHazards);

        public Task<List<SaftyHazard_RR_Link>> GetSafetyHazardLinkedToRR(int id);

        public Task<List<SafetyHazardWithLinkCount>> GetSafetyHazardLinkedToRRWithCount(int id);

        public Task<SaftyHazard_RR_Link> GetSafetyHazardLink(int rrId, int id);

        public Task<RegulatoryRequirement> LinkProcedure(int rrId, RR_Procedure_LinkOptions options);

        public Task<List<RegulatoryRequirementCompact>> getRRLinkedToSH(int id);

        public System.Threading.Tasks.Task UnlinkProcedure(int rrId, RR_Procedure_LinkOptions options);

        public Task<List<ProceduresWithLinkCount>> GetProcedureLinkedToRR(int id);

        public Task<List<RegulatoryRequirementCompact>> GetRRLinkedWithProcedure(int rrId);

        public Task<RegulatoryRequirement> LinkILA(int rrId, RR_ILA_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkILA(int rrId, RR_ILA_LinkOptions options);

        public Task<List<ILAProviderDataVM>> GetProviderWithILAs();

        public Task<List<ILATopicDataVM>> GetTopicWithILAs();

        public Task<List<ILAWithCountOptions>> GetLinkedILAs(int id);

        public Task<List<RegulatoryRequirement>> GetRRILAIsLinkedTo(int id);

        public Task<RegulatoryRequirement> LinkEnablingObjective(int rrId, RegRequirement_EO_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkEnablingObjective(int rrId, RR_EO_LinkOptions options);

        public Task<List<RegulatoryRequirement>> GetEnablingObjectiveLinkedToRR(int id);

        public Task<List<RegulatoryRequirementWithLinkCount>> GetLinkedEOsWithCount(int id);

        public Task<RegRequirement_EO_Link> GetEnablingObjectiveLink(int rrId, int id);

        public Task<RegulatoryRequirement> LinkTask(int rrId, RR_Task_LinkOptions taskId);

        public System.Threading.Tasks.Task UnlinkTask(int rrId, RR_Task_LinkOptions tasks);

        public Task<List<RR_Task_Link>> GetTaskLinkedToRR(int id);

        public Task<List<RegulatoryRequirementCompact>> getRRLinkedToTask(int id);

        public Task<List<TaskWithCountOptions>> GetLinkedTasksWithCount(int id);

        public Task<RR_Task_Link> GetTaskLink(int rrId, int id);

        public Task<RR_StatsVM> GetStatsCount();

        public Task<List<RR_IssuingAuthority>> GetNotLinkedTo(string option);

        public Task<List<RRLatestActivityVM>> GetHistoryAsync();

        public System.Threading.Tasks.Task RegulationDeactivateAsync(int id, RegulatoryRequirementOptions options);

        public System.Threading.Tasks.Task RegulationActivateAsync(int id, RegulatoryRequirementOptions options);

// active inactive rr and categories
        public Task<List<RegulatoryRequirement>> GetRRActiveInactive(string option);

        public Task<List<RR_IssuingAuthority>> GetIAActiveInactive(string option);

    }
}
