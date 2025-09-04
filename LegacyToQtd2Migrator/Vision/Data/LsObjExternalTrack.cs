using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsObjExternalTrack
{
    public decimal Id { get; set; }

    public decimal FkObjective { get; set; }

    public decimal FkEvent { get; set; }

    public decimal FkLearner { get; set; }

    public string ApiData { get; set; }

    public string CommentsFromLms { get; set; }

    public DateTime? Datelastmodified { get; set; }

    public virtual LsLearningEvent FkEventNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Objective FkObjectiveNavigation { get; set; }
}
