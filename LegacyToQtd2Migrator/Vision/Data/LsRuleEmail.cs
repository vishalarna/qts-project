using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsRuleEmail
{
    public decimal FkLsQualCard { get; set; }

    public string Email { get; set; }

    public virtual LsQualCard FkLsQualCardNavigation { get; set; }
}
