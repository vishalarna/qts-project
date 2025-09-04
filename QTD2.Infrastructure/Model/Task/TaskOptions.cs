using System;

namespace QTD2.Infrastructure.Model.Task
{
    public class TaskOptions
    {
        public bool IsSignificant { get; set; }

        public int[] ProcedureIds { get; set; }

        public int[] PositionIds { get; set; }

        public int[] EnablingObjectiveIds { get; set; }

        public int[] RegulatoryRequirementIds { get; set; }

        public string ActionType { get; set; }

        public string ChangeNotes { get; set; }

        public int[] SafetyHazardIds { get; set; }

        public int[] ILAIds { get; set; }

        public int[] TaskIds { get; set; }

        public int[] ToolIds { get; set; }

        public DateTime EffectiveDate { get; set; }
    }
}
