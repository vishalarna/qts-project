using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormActivityRtobjective
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormActivityId { get; set; }
        public int ObjectiveIndex { get; set; }
        public string ObjectiveId { get; set; }
        public bool? ObjectiveIdNull { get; set; }
        public int SuccessStatus { get; set; }
        public int CompletionStatus { get; set; }
        public decimal? ScoreScaled { get; set; }
        public decimal? ScoreRaw { get; set; }
        public decimal? ScoreMax { get; set; }
        public decimal? ScoreMin { get; set; }
        public decimal? ProgressMeasure { get; set; }
        public string Description { get; set; }
        public bool? DescriptionNull { get; set; }

        public virtual ScormActivityRt ScormActivityRt { get; set; }
    }
}
