using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormObjectSeqRuleCond
    {
        public int ScormObjectId { get; set; }
        public int SeqRuleId { get; set; }
        public int SeqRuleConditionId { get; set; }
        public int RuleCondition { get; set; }
        public string ReferencedObjective { get; set; }
        public decimal MeasureThreshold { get; set; }
        public int ConditionOperator { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormObjectSeqRule ScormObjectSeqRule { get; set; }
    }
}
