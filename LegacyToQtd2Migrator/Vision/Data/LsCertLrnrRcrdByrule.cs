using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertLrnrRcrdByrule
{
    public decimal Id { get; set; }

    public decimal FkCertId { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkCertRequirmentsId { get; set; }

    public decimal FkCertRequirmentsRuleId { get; set; }

    public decimal FkLrnrCertRecrdId { get; set; }

    public decimal FkLrnrCertReqrecrdId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal IsCurrentRuleRecord { get; set; }

    public decimal ReqruleStatus { get; set; }

    public decimal ReqruleRunningTotal { get; set; }

    public decimal ReqruleRunningCrryoverTotal { get; set; }

    public decimal FkCreatedby { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkLastModifiedby { get; set; }

    public DateTime DateModified { get; set; }

    public decimal ReqruleRunningTotalByrecdte { get; set; }

    public decimal ReqruleByrecdateLostcredits { get; set; }

    public decimal ReqruleByrecdateTocarryover { get; set; }

    public decimal ReqruleLostcredits { get; set; }

    public virtual LsCertification FkCert { get; set; }

    public virtual LsCertRequirement FkCertRequirments { get; set; }

    public virtual LsCertRequirementsRule FkCertRequirmentsRule { get; set; }

    public virtual Learner FkCreatedbyNavigation { get; set; }

    public virtual Learner FkLastModifiedbyNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsCertLrnrRecordBycert FkLrnrCertRecrd { get; set; }

    public virtual LsCertLrnrRcrdByreqmnt FkLrnrCertReqrecrd { get; set; }
}
