using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCertification
{
    public decimal Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Userdefinedid { get; set; }

    public decimal FkXrefLibId { get; set; }

    public decimal FkTimeframeBasisRenewal { get; set; }

    public decimal FkTimetocomplValidityPeriod { get; set; }

    public decimal FkTimetocomplGraceperiod { get; set; }

    public decimal FkLscertReqbasisStart { get; set; }

    public decimal OrgCategory { get; set; }

    public decimal IsqualcardCert { get; set; }

    public decimal AutomaticRouting { get; set; }

    public decimal FkCreatedby { get; set; }

    public DateTime CreateDate { get; set; }

    public decimal FkModifiedby { get; set; }

    public DateTime ModifiedDate { get; set; }

    public decimal Isactive { get; set; }

    public decimal Isnerccertification { get; set; }

    public virtual Learner FkCreatedbyNavigation { get; set; }

    public virtual LsCertReqruleBasis FkLscertReqbasisStartNavigation { get; set; }

    public virtual Learner FkModifiedbyNavigation { get; set; }

    public virtual LsCertTimeframeBasis FkTimeframeBasisRenewalNavigation { get; set; }

    public virtual LsTimeToComplete FkTimetocomplGraceperiodNavigation { get; set; }

    public virtual LsTimeToComplete FkTimetocomplValidityPeriodNavigation { get; set; }

    public virtual XrefLib FkXrefLib { get; set; }

    public virtual ICollection<LsCertJob> LsCertJobs { get; set; } = new List<LsCertJob>();

    public virtual ICollection<LsCertLrnrRcrdByreqmnt> LsCertLrnrRcrdByreqmnts { get; set; } = new List<LsCertLrnrRcrdByreqmnt>();

    public virtual ICollection<LsCertLrnrRcrdByrule> LsCertLrnrRcrdByrules { get; set; } = new List<LsCertLrnrRcrdByrule>();

    public virtual ICollection<LsCertLrnrRecordBycert> LsCertLrnrRecordBycerts { get; set; } = new List<LsCertLrnrRecordBycert>();

    public virtual ICollection<LsCertLrnrRecordBycrse> LsCertLrnrRecordBycrses { get; set; } = new List<LsCertLrnrRecordBycrse>();

    public virtual ICollection<LsCertLrnrrdBycrsemain> LsCertLrnrrdBycrsemains { get; set; } = new List<LsCertLrnrrdBycrsemain>();

    public virtual ICollection<LsCertLrnr> LsCertLrnrs { get; set; } = new List<LsCertLrnr>();

    public virtual ICollection<LsCertManscreCrditmain> LsCertManscreCrditmains { get; set; } = new List<LsCertManscreCrditmain>();

    public virtual ICollection<LsCertRequirement> LsCertRequirements { get; set; } = new List<LsCertRequirement>();

    public virtual ICollection<LsCertRequirementsRule> LsCertRequirementsRules { get; set; } = new List<LsCertRequirementsRule>();
}
