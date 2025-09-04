using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsLoginAttempt
{
    public decimal FkLearner { get; set; }

    public decimal Attempt { get; set; }

    public DateTime? LastAttempt { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }
}
