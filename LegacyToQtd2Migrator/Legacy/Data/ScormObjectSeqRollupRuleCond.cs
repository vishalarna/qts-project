using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormObjectSeqRollupRuleCond
    {
        public int ScormObjectId { get; set; }
        public int RollupRuleId { get; set; }
        public int RollupRuleConditionId { get; set; }
        public int ConditionOperator { get; set; }
        public int RuleCondition { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormObjectSeqRollupRule ScormObjectSeqRollupRule { get; set; }
    }
}
