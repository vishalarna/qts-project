using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormObjectSeqDatum
    {
        public ScormObjectSeqDatum()
        {
            ScormObjectSeqObjectives = new HashSet<ScormObjectSeqObjective>();
            ScormObjectSeqRollupRules = new HashSet<ScormObjectSeqRollupRule>();
            ScormObjectSeqRules = new HashSet<ScormObjectSeqRule>();
        }

        public int ScormObjectId { get; set; }
        public bool HidePrevious { get; set; }
        public bool HideContinue { get; set; }
        public bool HideExit { get; set; }
        public bool HideAbandon { get; set; }
        public bool ControlChoice { get; set; }
        public bool ControlChoiceExit { get; set; }
        public bool ControlFlow { get; set; }
        public bool ControlForwardOnly { get; set; }
        public bool UseCurrentAttemptObjInfo { get; set; }
        public bool UseCurrentAttemptProgInfo { get; set; }
        public bool ConstrainChoice { get; set; }
        public bool PreventActivation { get; set; }
        public bool LimitCondAttemptControl { get; set; }
        public int LimitCondAttemptLimit { get; set; }
        public bool LimitCondAttemptDurControl { get; set; }
        public string LimitCondAttemptDurLimit { get; set; }
        public bool RollupObjectiveSatisfied { get; set; }
        public decimal RollupObjMeasureWeight { get; set; }
        public bool RollupProgressCompletion { get; set; }
        public bool MeasureSatisfactionIfActive { get; set; }
        public int RequiredForSatisfied { get; set; }
        public int RequiredForNotSatisfied { get; set; }
        public int RequiredForCompleted { get; set; }
        public int RequiredForIncomplete { get; set; }
        public int SelectionTiming { get; set; }
        public bool SelectionCountStatus { get; set; }
        public int SelectionCount { get; set; }
        public int RandomizationTiming { get; set; }
        public bool RandomizeChildren { get; set; }
        public bool Tracked { get; set; }
        public bool CompletionSetByContent { get; set; }
        public bool ObjectiveSetByContent { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public bool HideSuspendAll { get; set; }
        public bool HideAbandonAll { get; set; }
        public bool HideExitAll { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormObject ScormObject { get; set; }
        public virtual ICollection<ScormObjectSeqObjective> ScormObjectSeqObjectives { get; set; }
        public virtual ICollection<ScormObjectSeqRollupRule> ScormObjectSeqRollupRules { get; set; }
        public virtual ICollection<ScormObjectSeqRule> ScormObjectSeqRules { get; set; }
    }
}
