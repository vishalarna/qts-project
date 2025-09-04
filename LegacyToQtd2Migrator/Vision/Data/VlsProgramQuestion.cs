using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsProgramQuestion
{
    public decimal FkProgram { get; set; }

    public decimal FkObjective { get; set; }

    public DateTime ObjectiveQuestionDateCreated { get; set; }

    public DateTime ObjectiveQuestionDateExpired { get; set; }

    public decimal FkQuestion { get; set; }

    public decimal Type { get; set; }

    public decimal? MustAppear { get; set; }

    public decimal? Time { get; set; }

    public decimal? Points { get; set; }

    public decimal? ItemId { get; set; }

    public decimal? IsPractice { get; set; }

    public DateTime QuestionImplDateCreated { get; set; }

    public DateTime QuestionImplDateExpired { get; set; }

    public DateTime ObjectiveImplDateCreated { get; set; }

    public DateTime ObjectiveImplDateExpired { get; set; }

    public DateTime SequencingDateCreated { get; set; }

    public DateTime SequencingDateExpired { get; set; }

    public DateTime QuestionStatusImplDateCreated { get; set; }

    public DateTime QuestionStatusImplDateExpired { get; set; }

    public DateTime? VlsQuestionSimDateCreated { get; set; }

    public DateTime? VlsQuestionSimDateExpired { get; set; }

    public DateTime VlsQuestionTypeDateCreated { get; set; }

    public DateTime VlsQuestionTypeDateExpired { get; set; }

    public decimal QuestionImplId { get; set; }

    public decimal ObjectiveImplId { get; set; }
}
