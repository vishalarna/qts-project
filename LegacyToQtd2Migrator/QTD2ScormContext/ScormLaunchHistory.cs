using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormLaunchHistory
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int LaunchHistoryId { get; set; }
        public int ScormRegistrationId { get; set; }
        public DateTime LaunchTime { get; set; }
        public DateTime? LaunchTimeUtc { get; set; }
        public DateTime? ExitTime { get; set; }
        public DateTime? ExitTimeUtc { get; set; }
        public string Completion { get; set; }
        public string Satisfaction { get; set; }
        public bool? MeasureStatus { get; set; }
        public decimal? NormalizedMeasure { get; set; }
        public long? ExperiencedDurationTracked { get; set; }
        public string HistoryLog { get; set; }
        public DateTime? UpdateDtUtc { get; set; }
        public DateTime? LastRuntimeUpdate { get; set; }
        public DateTime? LastRuntimeUpdateUtc { get; set; }

        public virtual ScormRegistration ScormRegistration { get; set; }
    }
}
