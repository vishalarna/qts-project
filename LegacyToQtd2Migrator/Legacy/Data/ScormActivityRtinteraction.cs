using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormActivityRtinteraction
    {
        public ScormActivityRtinteraction()
        {
            ScormActivityRtintCorrectResps = new HashSet<ScormActivityRtintCorrectResp>();
            ScormActivityRtintObjectives = new HashSet<ScormActivityRtintObjective>();
        }

        public int ScormActivityId { get; set; }
        public int InteractionIndex { get; set; }
        public string InteractionId { get; set; }
        public int? Type { get; set; }
        public DateTime? TimestampUtc { get; set; }
        public string TimestampText { get; set; }
        public bool? TimestampNull { get; set; }
        public decimal? Weighting { get; set; }
        public int? Result { get; set; }
        public decimal? ResultNumeric { get; set; }
        public long? Latency { get; set; }
        public string Description { get; set; }
        public bool? DescriptionNull { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormActivityRt ScormActivityRt { get; set; }
        public virtual ScormActivityRtintLearnerResp ScormActivityRtintLearnerResp { get; set; }
        public virtual ICollection<ScormActivityRtintCorrectResp> ScormActivityRtintCorrectResps { get; set; }
        public virtual ICollection<ScormActivityRtintObjective> ScormActivityRtintObjectives { get; set; }
    }
}
