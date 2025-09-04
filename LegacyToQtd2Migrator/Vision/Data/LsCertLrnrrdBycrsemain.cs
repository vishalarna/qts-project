using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertLrnrrdBycrsemain
{
    public decimal Id { get; set; }

    public decimal FkCertId { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkProgramid { get; set; }

    public decimal FkLearningEvent { get; set; }

    public decimal? LrnrCertificateId { get; set; }

    public string Title { get; set; }

    public string LocationWhereCrsegiven { get; set; }

    public string ExternalCertificateid { get; set; }

    public decimal FkCreatedby { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkLastModifiedby { get; set; }

    public DateTime DateModified { get; set; }

    public decimal RdomCrseEventId { get; set; }

    public decimal Ismanuallyscored { get; set; }

    public virtual LsCertification FkCert { get; set; }

    public virtual Learner FkCreatedbyNavigation { get; set; }

    public virtual Learner FkLastModifiedbyNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual Program FkProgram { get; set; }

    public virtual ICollection<LsCertLrnrRecordBycrse> LsCertLrnrRecordBycrses { get; set; } = new List<LsCertLrnrRecordBycrse>();

    public virtual ICollection<LsCertManscreCrditmain> LsCertManscreCrditmains { get; set; } = new List<LsCertManscreCrditmain>();
}
