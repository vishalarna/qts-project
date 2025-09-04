using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsObjectiveTrack
{
    public decimal Id { get; set; }

    public decimal FkObjective { get; set; }

    public decimal FkEvent { get; set; }

    public decimal FkLearner { get; set; }

    public decimal Marked { get; set; }

    public decimal Viewed { get; set; }

    public decimal LessonType { get; set; }

    public decimal Mastered { get; set; }

    public DateTime? DateMastered { get; set; }

    public decimal FkProgram { get; set; }

    public decimal? FkContent { get; set; }

    public DateTime? DateLastViewed { get; set; }

    public decimal? Sequence { get; set; }

    public decimal FkProgramImpl { get; set; }

    public decimal TuDoclinks { get; set; }

    public decimal ObjectiveDoclinks { get; set; }

    public virtual Content FkContentNavigation { get; set; }

    public virtual LsLearningEvent FkEventNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Objective FkObjectiveNavigation { get; set; }

    public virtual ProgramImpl FkProgramImplNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
