using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;

namespace QTD2.Application.Interfaces.Services.Shared
{
    public interface IVersioningService
    {
        public System.Threading.Tasks.Task VersionEnablingObjectiveAsync(EnablingObjective eo);

        public Task<Version_Procedure> VersionProcedureAsync(Procedure proc);

        public Task<Version_SaftyHazard> VersionSaftyHazardAsync(SaftyHazard sh);

        public Task<Version_ILA> VersionILAAsync(ILA ila, string ChangeNotes, DateTime? EffectiveDate, int state = 1);

        public Task<Version_TrainingGroup> VersionTrainingGroupAsync(TrainingGroup tg);

        public Task<Version_RegulatoryRequirement> VersionRRAsync(RegulatoryRequirement rr);

        public System.Threading.Tasks.Task VersionTaskAsync(Domain.Entities.Core.Task task);

        public Task<Version_Tool> VersionToolAsync(Tool tool);

        public System.Threading.Tasks.Task VersionTaskStepAsync(Task_Step step);

        public System.Threading.Tasks.Task VersionTaskQuestionAsync(Task_Question question);

        public System.Threading.Tasks.Task VersionTask_ToolLinkAsync(List<Task_Tool> tool);

        //public System.Threading.Tasks.Task VersionTaskShAsync(List<Task_SaftyHazard_Link> task_shs);

        //public System.Threading.Tasks.Task VersionTask_ProcedureLinkAsync(List<Task_Procedure_Link> task_procs);

        public System.Threading.Tasks.Task VersionTask_EOLinkAsync(List<Task_EnablingObjective_Link> task_eos);

        public System.Threading.Tasks.Task VersionTask_ILALinkAsync(List<Task_ILA_Link> task_ilas);

        //public System.Threading.Tasks.Task VersionTask_RR_LinkAsync(List<Task_RR_Link> task_rrs);

        public System.Threading.Tasks.Task VersionTask_TrainingGroupAsync(List<Task_TrainingGroup> task_tgs);

        public Task<List<Version_Task>> GetVersionedTasksAsync();

        public System.Threading.Tasks.Task VersionEnablingObjective_StepAsync(EnablingObjective_Step step);
        public System.Threading.Tasks.Task VersionEnablingObjective_QuestionAsync(EnablingObjective_Question step);
    }
}
