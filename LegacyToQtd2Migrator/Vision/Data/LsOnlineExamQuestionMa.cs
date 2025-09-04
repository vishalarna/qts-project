using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsOnlineExamQuestionMa
{
    public decimal FkQuestion { get; set; }

    public decimal FkProgram { get; set; }

    public decimal FkLearner { get; set; }

    public decimal? FkExam { get; set; }

    public decimal ItemSequence { get; set; }

    public decimal? Points { get; set; }

    public decimal? Response { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
