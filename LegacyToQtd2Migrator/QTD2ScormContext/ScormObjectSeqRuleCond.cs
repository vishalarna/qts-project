using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormObjectSeqRuleCond
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormObjectId { get; set; }
        public int SeqRuleId { get; set; }
        public int SeqRuleConditionId { get; set; }
        public int RuleCondition { get; set; }
        public string ReferencedObjective { get; set; }
        public decimal MeasureThreshold { get; set; }
        public int ConditionOperator { get; set; }

        public virtual ScormObjectSeqRule ScormObjectSeqRule { get; set; }
    }
}
