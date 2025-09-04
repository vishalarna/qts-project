using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class VlsQuestionType
{
    public decimal FkQuestion { get; set; }

    public decimal? Points { get; set; }

    public decimal QuestionType { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateExpired { get; set; }
}
