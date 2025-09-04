using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsLearnerLogin
{
    public decimal Id { get; set; }

    public decimal FkLearner { get; set; }

    public DateTime LoginDate { get; set; }

    public string Ipaddress { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }
}
