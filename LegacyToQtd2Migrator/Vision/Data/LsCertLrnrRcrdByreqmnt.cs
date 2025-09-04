using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertLrnrRcrdByreqmnt
{
    public decimal Id { get; set; }

    public decimal FkCertId { get; set; }

    public decimal FkLearner { get; set; }

    public decimal FkCertRequirmentsId { get; set; }

    public decimal FkLrnrCertRecrdId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? GracePeriodEndDate { get; set; }

    public decimal IsCurrentReqRecord { get; set; }

    public decimal ReqStatus { get; set; }

    public decimal ReqRunningTotal { get; set; }

    public decimal ReqRunningCarryoverTotal { get; set; }

    public decimal ReqPriorCertCarryover { get; set; }

    public decimal FkCreatedby { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkLastModifiedby { get; set; }

    public DateTime DateModified { get; set; }

    public decimal GraceprdRunningTotal { get; set; }

    public decimal GraceprdLostcredits { get; set; }

    public decimal ReqLostcredits { get; set; }

    public decimal ReqRulelostcreditsRuntotl { get; set; }

    public virtual LsCertification FkCert { get; set; }

    public virtual LsCertRequirement FkCertRequirments { get; set; }

    public virtual Learner FkCreatedbyNavigation { get; set; }

    public virtual Learner FkLastModifiedbyNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual LsCertLrnrRecordBycert FkLrnrCertRecrd { get; set; }

    public virtual ICollection<LsCertLrnrRcrdByrule> LsCertLrnrRcrdByrules { get; set; } = new List<LsCertLrnrRcrdByrule>();
}
