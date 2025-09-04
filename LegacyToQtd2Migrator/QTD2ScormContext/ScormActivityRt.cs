using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormActivityRt
    {
        public ScormActivityRt()
        {
            ScormActivityRtcomments = new HashSet<ScormActivityRtcomment>();
            ScormActivityRtinteractions = new HashSet<ScormActivityRtinteraction>();
            ScormActivityRtobjectives = new HashSet<ScormActivityRtobjective>();
        }

        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormActivityId { get; set; }
        public int CompletionStatus { get; set; }
        public int Credit { get; set; }
        public int Entry { get; set; }
        public int ExitMode { get; set; }
        public string Location { get; set; }
        public bool? LocationNull { get; set; }
        public int LessonMode { get; set; }
        public decimal? ProgressMeasure { get; set; }
        public decimal? ScoreRaw { get; set; }
        public decimal? ScoreMax { get; set; }
        public decimal? ScoreMin { get; set; }
        public decimal? ScoreScaled { get; set; }
        public int SuccessStatus { get; set; }
        public string SuspendData { get; set; }
        public bool? SuspendDataNull { get; set; }
        public string SuspendDataOverflow { get; set; }
        public bool? SuspendDataOverflowNull { get; set; }
        public long TotalTime { get; set; }
        public long TotalTimeTracked { get; set; }
        public decimal AudioLevel { get; set; }
        public string LanguagePreference { get; set; }
        public decimal DeliverySpeed { get; set; }
        public int AudioCaptioning { get; set; }

        public virtual ScormActivity ScormActivity { get; set; }
        public virtual ICollection<ScormActivityRtcomment> ScormActivityRtcomments { get; set; }
        public virtual ICollection<ScormActivityRtinteraction> ScormActivityRtinteractions { get; set; }
        public virtual ICollection<ScormActivityRtobjective> ScormActivityRtobjectives { get; set; }
    }
}
