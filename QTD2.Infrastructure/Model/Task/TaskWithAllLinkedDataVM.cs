using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using QTD2.Infrastructure.Model.EnablingObjective;
using QTD2.Infrastructure.Model.ILA;
using QTD2.Infrastructure.Model.Procedure;
using QTD2.Infrastructure.Model.RegulatoryRequirement;
using QTD2.Infrastructure.Model.SaftyHazard;
using QTD2.Infrastructure.Model.Task_Step;
using QTD2.Infrastructure.Model.Task_Suggestion;
using QTD2.Infrastructure.Model.Tool;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskWithAllLinkedDataVM
    {
        public List<TaskPositionWithCount> TaskPositionWithCount { get; set; } = new List<TaskPositionWithCount>();

        public List<ILAWithCountOptions> ILAWithCountOptions { get; set; } = new List<ILAWithCountOptions>();

        public List<EnablingObjectiveWithCountOptions> EnablingObjectiveWithCountOptions { get; set; } = new List<EnablingObjectiveWithCountOptions>();

        public List<ProceduresWithLinkCount> ProceduresWithLinkCounts { get; set; } = new List<ProceduresWithLinkCount>();

        public List<SafetyHazardWithLinkCount> SafetyHazardWithLinkCounts { get; set; } = new List<SafetyHazardWithLinkCount>();

        public List<TaskRRWithCount> RegulatoryRequirementWithLinkCounts { get; set; } = new List<TaskRRWithCount>();

        public List<TrainingGroup> TrainingGroups { get; set; } = new List<TrainingGroup>();

        public List<ToolDataVM> Tools { get; set; } = new List<ToolDataVM>();

        public List<Task_StepDescriptionVM> Task_Steps { get; set; } = new List<Task_StepDescriptionVM>();
       
        public List<Task_SuggestionOptions> Task_Suggestions { get; set; } = new List<Task_SuggestionOptions>();

    }
}
