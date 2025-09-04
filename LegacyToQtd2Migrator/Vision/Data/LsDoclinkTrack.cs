using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsDoclinkTrack
{
    public decimal FkProgram { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkDoclink { get; set; }

    public DateTime? DateCompleted { get; set; }

    public virtual Doclink FkDoclinkNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
