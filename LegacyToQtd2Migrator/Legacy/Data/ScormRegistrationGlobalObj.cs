using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormRegistrationGlobalObj
    {
        public int ScormRegistrationId { get; set; }
        public int ScormRegistrationObjId { get; set; }
        public string ObjectiveIdentifier { get; set; }
        public bool ObjectiveProgressStatus { get; set; }
        public bool ObjectiveSatisfiedStatus { get; set; }
        public bool ObjectiveMeasureStatus { get; set; }
        public decimal ObjectiveNormalizedMeasure { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public bool CompletionStatus { get; set; }
        public bool CompletionStatusValue { get; set; }
        public decimal? ScoreRaw { get; set; }
        public decimal? ScoreMax { get; set; }
        public decimal? ScoreMin { get; set; }
        public bool ProgressMeasureStatus { get; set; }
        public decimal? ProgressMeasure { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormRegistration ScormRegistration { get; set; }
    }
}
