using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsProctorSession
{
    public string SessionName { get; set; }

    public decimal FkLearner { get; set; }

    public string SessionCode { get; set; }

    public DateTime SessionStart { get; set; }

    public DateTime SessionEnd { get; set; }

    public decimal Duration { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }
}
