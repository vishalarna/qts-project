using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsVLessonOb
{
    public decimal FkVLessonId { get; set; }

    public decimal FkObjective { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CompleteDate { get; set; }

    public virtual Objective FkObjectiveNavigation { get; set; }

    public virtual LsVLesson FkVLesson { get; set; }
}
