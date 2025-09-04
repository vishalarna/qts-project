using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormActivityRtintCorrectResp
    {
        public int ScormActivityId { get; set; }
        public int InteractionIndex { get; set; }
        public int InteractionCorrectRespIndex { get; set; }
        public string CorrectResponse { get; set; }
        public string CorrectResponseOverflow { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormActivityRtinteraction ScormActivityRtinteraction { get; set; }
    }
}
