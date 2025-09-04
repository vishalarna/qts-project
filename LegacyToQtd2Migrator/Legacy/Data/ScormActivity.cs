using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormActivity
    {
        public ScormActivity()
        {
            ScormActivityObjectives = new HashSet<ScormActivityObjective>();
        }

        public int ScormActivityId { get; set; }
        public int ScormRegistrationId { get; set; }
        public int ScormObjectId { get; set; }
        public bool ActivityProgressStatus { get; set; }
        public int ActivityAttemptCount { get; set; }
        public bool AttemptProgressStatus { get; set; }
        public decimal AttemptCompletionAmount { get; set; }
        public bool AttemptCompletionStatus { get; set; }
        public bool Active { get; set; }
        public bool Suspended { get; set; }
        public bool Included { get; set; }
        public int Ordinal { get; set; }
        public bool SelectedChildren { get; set; }
        public bool RandomizedChildren { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public DateTime? FirstCompletionTimestampUtc { get; set; }
        public bool PrevAttemptProgressStatus { get; set; }
        public bool PrevAttemptCompletionStatus { get; set; }
        public bool AttemptedDuringThisAttempt { get; set; }
        public bool AttemptCompletionAmountStat { get; set; }
        public DateTime? ActivityStartTimestampUtc { get; set; }
        public DateTime? AttemptStartTimestampUtc { get; set; }
        public long ActivityAbsoluteDur { get; set; }
        public long AttemptAbsoluteDur { get; set; }
        public long ActivityExpDurTracked { get; set; }
        public long AttemptExpDurTracked { get; set; }
        public long ActivityExpDurReported { get; set; }
        public long AttemptExpDurReported { get; set; }
        public string AiccSessionId { get; set; }
        public bool? IsLatestAttempt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormObject ScormObject { get; set; }
        public virtual ScormRegistration ScormRegistration { get; set; }
        public virtual ScormActivityRt ScormActivityRt { get; set; }
        public virtual ICollection<ScormActivityObjective> ScormActivityObjectives { get; set; }
    }
}
