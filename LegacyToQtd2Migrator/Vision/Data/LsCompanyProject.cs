using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCompanyProject
{
    public decimal Id { get; set; }

    public decimal FkCompany { get; set; }

    public decimal FkProject { get; set; }

    public DateTime DateBegin { get; set; }

    public virtual LsCompany FkCompanyNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }
}
