using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCompany
{
    public decimal Id { get; set; }

    public string Company { get; set; }

    public decimal FkAddress { get; set; }

    public string Email { get; set; }

    public decimal SelfRegistration { get; set; }

    public virtual LsAddress FkAddressNavigation { get; set; }

    public virtual ICollection<Learner> Learners { get; set; } = new List<Learner>();

    public virtual ICollection<LsCatalog> LsCatalogs { get; set; } = new List<LsCatalog>();

    public virtual ICollection<LsCertJob> LsCertJobs { get; set; } = new List<LsCertJob>();

    public virtual ICollection<LsCompanyProject> LsCompanyProjects { get; set; } = new List<LsCompanyProject>();

    public virtual ICollection<LsLearnerPositionHist> LsLearnerPositionHists { get; set; } = new List<LsLearnerPositionHist>();

    public virtual ICollection<LsLearnerPosition> LsLearnerPositions { get; set; } = new List<LsLearnerPosition>();

    public virtual ICollection<LsOrgHierarchy> LsOrgHierarchies { get; set; } = new List<LsOrgHierarchy>();

    public virtual ICollection<LsOrgLevel> LsOrgLevels { get; set; } = new List<LsOrgLevel>();

    public virtual ICollection<LsOrg> LsOrgs { get; set; } = new List<LsOrg>();

    public virtual ICollection<LsSurvey> LsSurveys { get; set; } = new List<LsSurvey>();

    public virtual ICollection<LsTrainingReqItem> LsTrainingReqItems { get; set; } = new List<LsTrainingReqItem>();

    public virtual ICollection<Learner> FkLearners { get; set; } = new List<Learner>();

    public virtual ICollection<XrefLib> FkXrefLibs { get; set; } = new List<XrefLib>();
}
