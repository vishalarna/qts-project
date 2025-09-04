using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsProfileEvalType
{
    public decimal FkExamOnlineProfile { get; set; }

    public decimal RatingScaleId { get; set; }

    public decimal? ReasonCodeId { get; set; }

    public decimal? ClrId { get; set; }

    public virtual EvalType Clr { get; set; }

    public virtual ExamOnlineProfile FkExamOnlineProfileNavigation { get; set; }

    public virtual EvalType RatingScale { get; set; }

    public virtual EvalType ReasonCode { get; set; }
}
