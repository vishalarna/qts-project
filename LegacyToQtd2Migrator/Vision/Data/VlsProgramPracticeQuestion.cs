using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsProgramPracticeQuestion
{
    public decimal FkProgram { get; set; }

    public decimal ObSeq { get; set; }

    public DateTime SequencingDateCreated { get; set; }

    public DateTime SequencingDateExpired { get; set; }

    public decimal FkObjective { get; set; }

    public DateTime ObjectiveQuestionDateCreated { get; set; }

    public DateTime ObjectiveQuestionDateExpired { get; set; }

    public string Objective { get; set; }

    public DateTime ObjectiveImplDateCreated { get; set; }

    public DateTime ObjectiveImplDateExpired { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal Type { get; set; }

    public decimal? Time { get; set; }

    public string Topic { get; set; }

    public DateTime QuestionImplDateCreated { get; set; }

    public DateTime QuestionImplDateExpired { get; set; }

    public decimal? Points { get; set; }

    public DateTime VlsQuestionTypeDateCreated { get; set; }

    public DateTime VlsQuestionTypeDateExpired { get; set; }

    public byte[] Stem { get; set; }

    public DateTime QuestionHtmlDateCreated { get; set; }

    public DateTime QuestionHtmlDateExpired { get; set; }
}
