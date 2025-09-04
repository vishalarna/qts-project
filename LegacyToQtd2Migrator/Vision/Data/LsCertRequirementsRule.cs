using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertRequirementsRule
{
    public decimal Id { get; set; }

    public decimal FkCertId { get; set; }

    public decimal FkCertRequirmentsId { get; set; }

    public decimal CriteriaThisRule { get; set; }

    public decimal MaxCarryoverAllowed { get; set; }

    public decimal CarryoverTimeTotal { get; set; }

    public decimal FkTimetypeCarryoverBasis { get; set; }

    public decimal FkLscertCarryovrbasisStart { get; set; }

    public decimal MaxCourserepeatAllowed { get; set; }

    public decimal CourserepeatTimeTotal { get; set; }

    public decimal FkTimetypeCrserepeatBasis { get; set; }

    public decimal FkLscertCrserptbasisStart { get; set; }

    public decimal Sequence { get; set; }

    public decimal FkCreatedby { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkLastModifiedby { get; set; }

    public DateTime DateModified { get; set; }

    public decimal Isactive { get; set; }

    public decimal CrryovrApplyedAfterReqtotl { get; set; }

    public virtual LsCertification FkCert { get; set; }

    public virtual LsCertRequirement FkCertRequirments { get; set; }

    public virtual Learner FkCreatedbyNavigation { get; set; }

    public virtual Learner FkLastModifiedbyNavigation { get; set; }

    public virtual LsCertReqruleBasis FkLscertCarryovrbasisStartNavigation { get; set; }

    public virtual LsCertReqruleBasis FkLscertCrserptbasisStartNavigation { get; set; }

    public virtual TimeType FkTimetypeCarryoverBasisNavigation { get; set; }

    public virtual TimeType FkTimetypeCrserepeatBasisNavigation { get; set; }

    public virtual ICollection<LsCertLrnrRcrdByrule> LsCertLrnrRcrdByrules { get; set; } = new List<LsCertLrnrRcrdByrule>();
}
