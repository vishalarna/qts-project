using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsPasswordStorage
{
    public decimal FkLearner { get; set; }

    public string OldPassword { get; set; }

    public DateTime ArchiveDate { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }
}
