using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormActivityObjective
    {
        public int ScormActivityId { get; set; }
        public int ScormActivityObjectiveId { get; set; }
        public string ObjectiveIdentifier { get; set; }
        public bool ObjectiveProgressStatus { get; set; }
        public bool ObjectiveSatisfiedStatus { get; set; }
        public bool ObjectiveMeasureStatus { get; set; }
        public decimal ObjectiveNormalizedMeasure { get; set; }
        public bool PrimaryObjective { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public DateTime? FirstSuccessTimestampUtc { get; set; }
        public decimal? FirstObjNormalizedMeasure { get; set; }
        public bool PrevObjProgressStatus { get; set; }
        public bool PrevObjSatisfiedStatus { get; set; }
        public bool PrevObjMeasureStatus { get; set; }
        public decimal PrevObjNormalizedMeasure { get; set; }
        public bool CompletionStatus { get; set; }
        public bool CompletionStatusValue { get; set; }
        public decimal? ScoreRaw { get; set; }
        public decimal? ScoreMax { get; set; }
        public decimal? ScoreMin { get; set; }
        public bool ProgressMeasureStatus { get; set; }
        public decimal? ProgressMeasure { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormActivity ScormActivity { get; set; }
    }
}
