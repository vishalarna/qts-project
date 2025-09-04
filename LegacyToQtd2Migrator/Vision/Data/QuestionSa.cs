using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class QuestionSa
{
    public decimal FkQuestion { get; set; }

    public decimal Points { get; set; }

    public string Answer { get; set; }

    public decimal? WhiteSpace { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual Question FkQuestionNavigation { get; set; }
}
