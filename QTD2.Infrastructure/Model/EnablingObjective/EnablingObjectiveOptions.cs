using System;

namespace QTD2.Infrastructure.Model.EnablingObjective
{
    public class EnablingObjectiveOptions
    {
        public string ActionType { get; set; }

        public string ChangeNotes { get; set; }

        public DateTime EffectiveDate { get; set; }

        public int[] EOIDs { get; set; }

        public int[] ToolIds { get; set; }
    }
}
