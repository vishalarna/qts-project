using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsDeveloperToLearner
{
    public decimal FkDeveloper { get; set; }

    public decimal FkLearner { get; set; }

    public virtual Developer FkDeveloperNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }
}
