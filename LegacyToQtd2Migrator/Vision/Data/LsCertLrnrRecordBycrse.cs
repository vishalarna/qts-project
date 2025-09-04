using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertLrnrRecordBycrse
{
    public decimal Id { get; set; }

    public decimal RdomCrseEventId { get; set; }

    public decimal FkCertId { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkCertRequirmentsId { get; set; }

    public decimal FkXrefLibId { get; set; }

    public decimal FkProgramid { get; set; }

    public decimal FkLearningEvent { get; set; }

    public decimal TotalValueOfCourse { get; set; }

    public decimal IsvalidForCredit { get; set; }

    public decimal FkCreatedby { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkLastModifiedby { get; set; }

    public DateTime DateModified { get; set; }

    public string LocationWhereCrsegiven { get; set; }

    public decimal ReceivedValue { get; set; }

    public decimal FkLsCertLrnrrdBycrsemain { get; set; }

    public decimal Ismanuallyscored { get; set; }

    public decimal FkProgramCompletion { get; set; }

    public virtual LsCertification FkCert { get; set; }

    public virtual LsCertRequirement FkCertRequirments { get; set; }

    public virtual Learner FkCreatedbyNavigation { get; set; }

    public virtual Learner FkLastModifiedbyNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsLearningEvent FkLearningEventNavigation { get; set; }

    public virtual LsCertLrnrrdBycrsemain FkLsCertLrnrrdBycrsemainNavigation { get; set; }

    public virtual Program FkProgram { get; set; }

    public virtual LsProgramCompletion FkProgramCompletionNavigation { get; set; }

    public virtual XrefLib FkXrefLib { get; set; }

    public virtual ICollection<LsCertManscreCrdit> LsCertManscreCrdits { get; set; } = new List<LsCertManscreCrdit>();
}
