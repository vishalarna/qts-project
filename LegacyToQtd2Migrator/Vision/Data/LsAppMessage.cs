using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsAppMessage
{
    public decimal SerialNumber { get; set; }

    public decimal FkMessageCategory { get; set; }

    public string Message { get; set; }

    public virtual ICollection<ExamOnlineProfileMessage> ExamOnlineProfileMessages { get; set; } = new List<ExamOnlineProfileMessage>();

    public virtual LsAppMessageCategory FkMessageCategoryNavigation { get; set; }
}
