using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamProfileByObjective
{
    public decimal FkExam { get; set; }

    public decimal FkObjectiveLevel { get; set; }

    public decimal PercentToTest { get; set; }

    public decimal QuestionsToTest { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual ObjectiveLevel FkObjectiveLevelNavigation { get; set; }
}
