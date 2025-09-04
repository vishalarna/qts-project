using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormObjectSeqObjective
    {
        public ScormObjectSeqObjective()
        {
            ScormObjectSeqObjectiveMaps = new HashSet<ScormObjectSeqObjectiveMap>();
        }

        public int ScormObjectId { get; set; }
        public int ScormObjectSeqObjectiveId { get; set; }
        public string ObjectiveIdentifier { get; set; }
        public bool SatisfiedByMeasure { get; set; }
        public decimal MinNormalizedMeasure { get; set; }
        public bool PrimaryObjective { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormObjectSeqDatum ScormObjectSeqDatum { get; set; }
        public virtual ICollection<ScormObjectSeqObjectiveMap> ScormObjectSeqObjectiveMaps { get; set; }
    }
}
