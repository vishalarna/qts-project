using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamEvent
{
    public decimal FkExam { get; set; }

    public decimal? FkProctor { get; set; }

    public decimal FkLearner { get; set; }

    public DateTime DateStarted { get; set; }

    public DateTime? DateCompleted { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Learner FkProctorNavigation { get; set; }
}
