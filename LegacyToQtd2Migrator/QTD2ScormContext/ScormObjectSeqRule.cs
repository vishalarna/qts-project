using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormObjectSeqRule
    {
        public ScormObjectSeqRule()
        {
            ScormObjectSeqRuleConds = new HashSet<ScormObjectSeqRuleCond>();
        }

        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormObjectId { get; set; }
        public int SeqRuleId { get; set; }
        public int ConditionCombination { get; set; }
        public int Action { get; set; }
        public int RuleType { get; set; }

        public virtual ScormObjectSeqDatum ScormObjectSeqDatum { get; set; }
        public virtual ICollection<ScormObjectSeqRuleCond> ScormObjectSeqRuleConds { get; set; }
    }
}
