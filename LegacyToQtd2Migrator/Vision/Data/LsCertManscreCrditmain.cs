using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertManscreCrditmain
{
    public decimal Id { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkLsCertifications { get; set; }

    public decimal FkLsLearningEvent { get; set; }

    public decimal FkProgram { get; set; }

    public decimal FkLsCertLrnrrdBycrsemain { get; set; }

    public decimal FkCreatedby { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkLastModifiedby { get; set; }

    public DateTime DateModified { get; set; }

    public virtual Learner FkCreatedbyNavigation { get; set; }

    public virtual Learner FkLastModifiedbyNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsCertLrnrrdBycrsemain FkLsCertLrnrrdBycrsemainNavigation { get; set; }

    public virtual LsCertification FkLsCertificationsNavigation { get; set; }

    public virtual LsLearningEvent FkLsLearningEventNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual ICollection<LsCertManscreCrdit> LsCertManscreCrdits { get; set; } = new List<LsCertManscreCrdit>();
}
