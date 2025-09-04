using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsOnlineExamQuestionMc
{
    public decimal FkQuestion { get; set; }

    public decimal FkProgram { get; set; }

    public decimal FkLearner { get; set; }

    public decimal PositionOnExam { get; set; }

    public decimal SequenceOnQuestionMc { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
