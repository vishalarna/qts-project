using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertRequirement
{
    public decimal Id { get; set; }

    public decimal FkCertId { get; set; }

    public string Description { get; set; }

    public decimal FkXrefLibId { get; set; }

    public decimal CriteriaTotal { get; set; }

    public decimal IsCriteriatotalRestricted { get; set; }

    public decimal FkTimeframeBasisReq { get; set; }

    public decimal FkLscertReqbasisStart { get; set; }

    public decimal IsvalidprdSameasCertrecrd { get; set; }

    public decimal FkTimetocomplValidityperiod { get; set; }

    public decimal PeriodBreakdownForRules { get; set; }

    public decimal HonorCertGracePeriod { get; set; }

    public decimal SortOrder { get; set; }

    public decimal FkCreatedby { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkLastModifiedby { get; set; }

    public DateTime DateModified { get; set; }

    public decimal Isactive { get; set; }

    public decimal LrnrRecordDisplayPeriod { get; set; }

    public virtual LsCertification FkCert { get; set; }

    public virtual Learner FkCreatedbyNavigation { get; set; }

    public virtual Learner FkLastModifiedbyNavigation { get; set; }

    public virtual LsCertReqruleBasis FkLscertReqbasisStartNavigation { get; set; }

    public virtual LsCertTimeframeBasis FkTimeframeBasisReqNavigation { get; set; }

    public virtual LsTimeToComplete FkTimetocomplValidityperiodNavigation { get; set; }

    public virtual XrefLib FkXrefLib { get; set; }

    public virtual ICollection<LsCertLrnrRcrdByreqmnt> LsCertLrnrRcrdByreqmnts { get; set; } = new List<LsCertLrnrRcrdByreqmnt>();

    public virtual ICollection<LsCertLrnrRcrdByrule> LsCertLrnrRcrdByrules { get; set; } = new List<LsCertLrnrRcrdByrule>();

    public virtual ICollection<LsCertLrnrRecordBycrse> LsCertLrnrRecordBycrses { get; set; } = new List<LsCertLrnrRecordBycrse>();

    public virtual ICollection<LsCertRequirementsRule> LsCertRequirementsRules { get; set; } = new List<LsCertRequirementsRule>();
}
