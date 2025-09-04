using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormActivityRtintLearnerResp
    {
        public int ScormActivityId { get; set; }
        public int InteractionIndex { get; set; }
        public string LearnerResponse { get; set; }
        public bool? LearnerResponseNull { get; set; }
        public string LearnerResponseOverflow { get; set; }
        public bool? LearnerResponseOverflowNull { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormActivityRtinteraction ScormActivityRtinteraction { get; set; }
    }
}
