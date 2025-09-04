using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormObjectSeqObjectiveMap
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormObjectId { get; set; }
        public int ScormObjectSeqObjectiveId { get; set; }
        public int ScormObjectSeqObjMapId { get; set; }
        public string TargetObjectiveId { get; set; }
        public bool ReadSatisfiedStatus { get; set; }
        public bool ReadNormalizedMeasure { get; set; }
        public bool WriteSatisfiedStatus { get; set; }
        public bool WriteNormalizedMeasure { get; set; }
        public bool ReadScoreRaw { get; set; }
        public bool ReadScoreMin { get; set; }
        public bool ReadScoreMax { get; set; }
        public bool ReadCompletionStatus { get; set; }
        public bool ReadProgressMeasure { get; set; }
        public bool WriteScoreRaw { get; set; }
        public bool WriteScoreMin { get; set; }
        public bool WriteScoreMax { get; set; }
        public bool WriteCompletionStatus { get; set; }
        public bool WriteProgressMeasure { get; set; }

        public virtual ScormObjectSeqObjective ScormObjectSeqObjective { get; set; }
    }
}
