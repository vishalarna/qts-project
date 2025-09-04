using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsRule
{
    public decimal FkLsQualCard { get; set; }

    public decimal FkRule { get; set; }

    public decimal Type { get; set; }

    public virtual LsQualCard FkLsQualCardNavigation { get; set; }

    public virtual LsRuleItem FkRuleNavigation { get; set; }
}
