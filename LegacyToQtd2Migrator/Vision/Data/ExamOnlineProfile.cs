using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamOnlineProfile
{
    public decimal Id { get; set; }

    public decimal? QuestionSampleType { get; set; }

    public decimal AllMustAppear { get; set; }

    public decimal? PassingScore { get; set; }

    public string Title { get; set; }

    public DateTime? DateCreated { get; set; }

    public decimal? QuestionSampleValue { get; set; }

    public decimal? QuestionSampleMin { get; set; }

    public decimal Archived { get; set; }

    public decimal Proctored { get; set; }

    public decimal Remediate { get; set; }

    public decimal TestLock { get; set; }

    public string Email1 { get; set; }

    public string Email2 { get; set; }

    public string Email3 { get; set; }

    public decimal Scoring { get; set; }

    public decimal EmailResponse { get; set; }

    public decimal DigitalSignature { get; set; }

    public decimal SendmailLearner { get; set; }

    public decimal SendmailInstructor { get; set; }

    public decimal SendmailAdmin { get; set; }

    public decimal SendmailSiteadmin { get; set; }

    public decimal SendmailEmail1 { get; set; }

    public decimal SendmailEmail2 { get; set; }

    public decimal SendmailEmail3 { get; set; }

    public decimal SendmailSupervisor { get; set; }

    public string SupervisorEmailText { get; set; }

    public string Alt1EmailText { get; set; }

    public string Alt2EmailText { get; set; }

    public string LearnerEmailText { get; set; }

    public decimal? FkEval { get; set; }

    public decimal FkOrigExamOnlineProfile { get; set; }

    public decimal Approved { get; set; }

    public decimal? FailedRemediation { get; set; }

    public decimal? PassedRemediation { get; set; }

    public virtual ICollection<ExamOnlineProfileMessage> ExamOnlineProfileMessages { get; set; } = new List<ExamOnlineProfileMessage>();

    public virtual Eval FkEvalNavigation { get; set; }

    public virtual ExamOnlineProfile FkOrigExamOnlineProfileNavigation { get; set; }

    public virtual ICollection<ExamOnlineProfile> InverseFkOrigExamOnlineProfileNavigation { get; set; } = new List<ExamOnlineProfile>();

    public virtual ICollection<LsCatalogLesson> LsCatalogLessons { get; set; } = new List<LsCatalogLesson>();

    public virtual ICollection<LsExamEvent> LsExamEvents { get; set; } = new List<LsExamEvent>();

    public virtual LsProfileEvalType LsProfileEvalType { get; set; }

    public virtual ICollection<Program> FkPrograms { get; set; } = new List<Program>();
}
