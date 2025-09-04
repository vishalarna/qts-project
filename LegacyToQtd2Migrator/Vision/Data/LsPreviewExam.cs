using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsPreviewExam
{
    public decimal FkExam { get; set; }

    public decimal FkProgram { get; set; }

    public decimal FkLearner { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }
}
