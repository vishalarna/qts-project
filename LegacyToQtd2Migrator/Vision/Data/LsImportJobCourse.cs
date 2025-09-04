using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsImportJobCourse
{
    public decimal Id { get; set; }

    public string JobDutyTask { get; set; }

    public string CourseCode { get; set; }

    public decimal? CourseInterval { get; set; }

    public decimal? Grace { get; set; }

    public decimal RequalModifier { get; set; }

    public decimal Xfer { get; set; }

    public DateTime? DateXfer { get; set; }

    public string XferMessage { get; set; }

    public virtual LsDateModifier RequalModifierNavigation { get; set; }
}
