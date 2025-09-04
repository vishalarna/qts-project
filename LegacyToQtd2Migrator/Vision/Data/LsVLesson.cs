using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsVLesson
{
    public decimal Programid { get; set; }

    public string Title { get; set; }

    public decimal FkLearner { get; set; }

    public DateTime? CreateDate { get; set; }

    public decimal Active { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual ICollection<LsVLessonOb> LsVLessonObs { get; set; } = new List<LsVLessonOb>();
}
