using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsRuleItem
{
    public decimal Id { get; set; }

    public string Text { get; set; }

    public decimal Itemvalue { get; set; }

    public decimal Type { get; set; }

    public decimal? Sequence { get; set; }

    public virtual ICollection<LsRule> LsRules { get; set; } = new List<LsRule>();
}
