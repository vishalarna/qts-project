using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertLrnrRecordBycert
{
    public decimal Id { get; set; }

    public decimal FkCertId { get; set; }

    public decimal FkLearner { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal IsCurrentCertRecord { get; set; }

    public decimal CertStatus { get; set; }

    public string AgencyCertificationId { get; set; }

    public DateTime AgencyIssueDate { get; set; }

    public DateTime? GracePeriodEndDate { get; set; }

    public decimal FkCreatedby { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkLastModifiedby { get; set; }

    public DateTime DateModified { get; set; }

    public virtual LsCertification FkCert { get; set; }

    public virtual Learner FkCreatedbyNavigation { get; set; }

    public virtual Learner FkLastModifiedbyNavigation { get; set; }

    public virtual Learner FkLearnerNavigation { get; set; }

    public virtual ICollection<LsCertLrnrRcrdByreqmnt> LsCertLrnrRcrdByreqmnts { get; set; } = new List<LsCertLrnrRcrdByreqmnt>();

    public virtual ICollection<LsCertLrnrRcrdByrule> LsCertLrnrRcrdByrules { get; set; } = new List<LsCertLrnrRcrdByrule>();
}
