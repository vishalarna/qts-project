using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsQuestionScenario
{
    public decimal QuestionId { get; set; }

    public decimal SubQuestionId { get; set; }

    public decimal Type { get; set; }

    public DateTime QuestionScDateCreated { get; set; }

    public DateTime QuestionScDateExpired { get; set; }

    public decimal? Points { get; set; }

    public DateTime VlsSubQuestionTypeDateCreated { get; set; }

    public DateTime VlsSubQuestionTypeDateExpired { get; set; }
}
