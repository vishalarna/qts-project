using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsSurveyObjectType
{
    public decimal IdValue { get; set; }

    public string Object { get; set; }

    public string ObjTable { get; set; }

    public string ObjTableColumn { get; set; }

    public virtual ICollection<LsSurveyItem> LsSurveyItems { get; set; } = new List<LsSurveyItem>();

    public virtual ICollection<LsSurveyeventItem> LsSurveyeventItems { get; set; } = new List<LsSurveyeventItem>();
}
