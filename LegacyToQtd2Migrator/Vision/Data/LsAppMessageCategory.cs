using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsAppMessageCategory
{
    public decimal Id { get; set; }

    public string Description { get; set; }

    public virtual ICollection<ExamOnlineProfileMessage> ExamOnlineProfileMessages { get; set; } = new List<ExamOnlineProfileMessage>();

    public virtual ICollection<LsAppMessage> LsAppMessages { get; set; } = new List<LsAppMessage>();
}
