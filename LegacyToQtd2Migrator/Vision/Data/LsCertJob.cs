using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertJob
{
    public decimal Id { get; set; }

    public decimal FkCertId { get; set; }

    public decimal FkCompanyId { get; set; }

    public decimal FkJobId { get; set; }

    public decimal FkOrgParentnodeId { get; set; }

    public decimal FkCreatedby { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkLastModifiedby { get; set; }

    public DateTime DateModified { get; set; }

    public decimal Isactive { get; set; }

    public virtual LsCertification FkCert { get; set; }

    public virtual LsCompany FkCompany { get; set; }

    public virtual Learner FkCreatedbyNavigation { get; set; }

    public virtual Learner FkLastModifiedbyNavigation { get; set; }
}
