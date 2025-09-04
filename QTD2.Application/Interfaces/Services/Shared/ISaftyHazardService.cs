using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.ILA_Topic;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.SafetyHazard_EO_Link;
using QTD2.Infrastructure.Model.SafetyHazard_ILA_Link;
using QTD2.Infrastructure.Model.SafetyHazard_Procedure_Link;
using QTD2.Infrastructure.Model.SafetyHazard_Set;
using QTD2.Infrastructure.Model.SafetyHazard_Set_Link;
using QTD2.Infrastructure.Model.SafetyHazard_Task_Link;
using QTD2.Infrastructure.Model.SafetyHazard_Tool_Link;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.SaftyHazard_Abatement;
using QTD2.Infrastructure.Model.SaftyHazard_Control;
using QTD2.Infrastructure.Model.SaftyHazard_RR_Link;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface ISaftyHazardService
    {
        public Task<List<SaftyHazard>> GetAsync();

        public Task<SaftyHazard> GetAsync(int id);

        public Task<SaftyHazardWithSet> GetWithSetsAsync(int id);

        public Task<SH_StatsVM> GetSHStats();

        public Task<List<Tool>> GetToolsLinkedToShAsync(int id);

        public Task<SaftyHazard> CreateAsync(SaftyHazardCreateOptions options);

        public Task<SaftyHazard> UpdateAsync(int id, SaftyHazardCreateOptions options);

        public Task<SaftyHazard> CopySafetyHazardWithLinkages(int id, SaftyHazardCreateOptions options);

        public System.Threading.Tasks.Task UpdateDescriptionAsync(int id, SaftyHazardCreateOptions option);

        public System.Threading.Tasks.Task DeleteAsync(SaftyHazardOptions options);

        public System.Threading.Tasks.Task DeactivateAsync(SaftyHazardOptions options);

        public System.Threading.Tasks.Task ActivateAsync(SaftyHazardOptions options);

        public Task<List<SaftyHazard>> GetSHWithSHCatId(int id);

        public Task<List<SaftyHazard_Category>> GetSHNotLinkedTo(string option);

        public Task<SaftyHazard_Abatement> AddAbatementAsync(int saftyHazardId, SaftyHazard_AbatementCreateOptions options);

        public Task<SaftyHazard_Abatement> RemoveAbatementAsync(int saftyHazardId, int saftyHazardAbatementId);

        public Task<SaftyHazard_Control> AddControlAsync(int saftyHazardId, SaftyHazard_ControlCreateOptions options);

        public Task<SaftyHazard_Control> RemoveControlAsync(int saftyHazardId, int saftyHazardControlId);

        public Task<SaftyHazard> LinkEO(int id, SafetyHazard_EO_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkEO(int shId, SafetyHazard_EO_LinkOptions options);

        public Task<List<SafetyHazardWithLinkCount>> GetLinkedEOsWithCount(int id);

        public Task<List<SaftyHazardCompactOptions>> getSHLinkedToEO(int id);

        public Task<SaftyHazard> LinkTask(int id, SafetyHazard_Task_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkTask(int shId, SafetyHazard_Task_LinkOptions options);

        public Task<List<SafetyHazardWithLinkCount>> GetLinkedTasksWithCount(int id);

        public Task<List<SaftyHazardCompactOptions>> getSHLinkedToTask(int id);

        public Task<SaftyHazard> LinkSet(int id, SafetyHazard_Set_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkSet(int shId, int shSetId);

        public Task<SaftyHazard> LinkILA(int id, SafetyHazard_ILA_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkILA(int shId, SafetyHazard_ILA_LinkOptions options);

        public Task<List<SafetyHazardWithLinkCount>> GetLinkedILAsWithCount(int id);

        public Task<List<SaftyHazardCompactOptions>> getSHLinkedToILA(int id);

        public Task<List<SaftyHazardCompactOptions>> getSHLinkedToProcedure(int id);

        public Task<List<SaftyHazardCompactOptions>> getSHLinkedToRR(int id);

        public Task<List<ProviderIlaVM>> GetProviderWithILAs();

        public Task<List<ILATopicDataVM>> GetTopicWithILAs();

        public Task<SaftyHazard> LinkProcedure(int id, SafetyHazard_Procedure_LinkOptions options);

        public Task<List<ProceduresWithLinkCount>> GetLinkedProcedureWithCount(int id);

        public System.Threading.Tasks.Task UnlinkProcedure(int shId, SafetyHazard_Procedure_LinkOptions proc);

        public Task<SaftyHazard> LinkRR(int id, SaftyHazard_RR_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkRR(int shId, SaftyHazard_RR_LinkOptions options);

        public Task<List<RegulatoryRequirementWithLinkCount>> getLinkedRRWithCount(int id);

        public Task<SaftyHazard> LinkPPETool(int id, SafetyHazard_Tool_LinkOptions options);

        public System.Threading.Tasks.Task UnlinkPPETool(int shId, SafetyHazard_Tool_LinkOptions options);

        //active inactive list for sh and categories
        public Task<List<SaftyHazard>> GetSHActiveInactive(string option);

        public Task<List<SaftyHazard_Category>> GetIAActiveInactive(string option);


    }
}
