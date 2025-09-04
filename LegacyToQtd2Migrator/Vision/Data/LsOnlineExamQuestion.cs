using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsOnlineExamQuestion
{
    public decimal FkQuestion { get; set; }

    public decimal FkProgram { get; set; }

    public decimal FkLearner { get; set; }

    public decimal? FkExam { get; set; }

    public decimal? Flag { get; set; }

    public decimal? IsSubquestion { get; set; }

    public decimal? VdmSequence { get; set; }

    public decimal? VlsSequence { get; set; }

    public decimal? QuestionType { get; set; }

    public decimal? FkObjectiveParent { get; set; }

    public decimal? FkQuestionParent { get; set; }

    public decimal? Response { get; set; }

    public decimal? ResponseType { get; set; }

    public decimal? Points { get; set; }

    public decimal? Score { get; set; }

    public virtual Exam FkExamNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual Objective FkObjectiveParentNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }

    public virtual Question FkQuestionParentNavigation { get; set; }
}
