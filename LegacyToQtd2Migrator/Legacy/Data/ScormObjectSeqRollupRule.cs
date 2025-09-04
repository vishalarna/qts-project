using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormObjectSeqRollupRule
    {
        public ScormObjectSeqRollupRule()
        {
            ScormObjectSeqRollupRuleConds = new HashSet<ScormObjectSeqRollupRuleCond>();
        }

        public int ScormObjectId { get; set; }
        public int RollupRuleId { get; set; }
        public int ConditionCombination { get; set; }
        public int ChildActivitySet { get; set; }
        public int MinimumCount { get; set; }
        public decimal MinimumPercent { get; set; }
        public int Action { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormObjectSeqDatum ScormObjectSeqDatum { get; set; }
        public virtual ICollection<ScormObjectSeqRollupRuleCond> ScormObjectSeqRollupRuleConds { get; set; }
    }
}
