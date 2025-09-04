using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ExamOnlineProfileMessage
{
    public decimal FkExamOnlineProfile { get; set; }

    public decimal FkMessage { get; set; }

    public decimal FkMessageCategory { get; set; }

    public virtual ExamOnlineProfile FkExamOnlineProfileNavigation { get; set; }

    public virtual LsAppMessageCategory FkMessageCategoryNavigation { get; set; }

    public virtual LsAppMessage FkMessageNavigation { get; set; }
}
