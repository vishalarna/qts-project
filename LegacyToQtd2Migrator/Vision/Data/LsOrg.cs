using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsOrg
{
    public decimal Id { get; set; }

    public decimal FkCompany { get; set; }

    public decimal FkOrgLevel { get; set; }

    public string Text { get; set; }

    public string UserDefinedId { get; set; }

    public string CrossReference { get; set; }

    public decimal FkOrgStatus { get; set; }

    public DateTime? DateCreated { get; set; }

    public decimal? FkCreatedBy { get; set; }

    public decimal? IsJob { get; set; }

    public decimal? PersonnelRequired { get; set; }

    public virtual LsCompany FkCompanyNavigation { get; set; }

    public virtual Learner FkCreatedByNavigation { get; set; }

    public virtual LsOrgLevel FkOrgLevelNavigation { get; set; }

    public virtual ICollection<LsCatalog> LsCatalogs { get; set; } = new List<LsCatalog>();

    public virtual ICollection<LsLearnerPositionHist> LsLearnerPositionHists { get; set; } = new List<LsLearnerPositionHist>();

    public virtual ICollection<LsLearnerPosition> LsLearnerPositions { get; set; } = new List<LsLearnerPosition>();

    public virtual LsOrgHierarchy LsOrgHierarchyFkChildNavigation { get; set; }

    public virtual ICollection<LsOrgHierarchy> LsOrgHierarchyFkParentNavigations { get; set; } = new List<LsOrgHierarchy>();

    public virtual ICollection<LsOrgPanelsTopNode> LsOrgPanelsTopNodes { get; set; } = new List<LsOrgPanelsTopNode>();

    public virtual ICollection<LsQualCardPrerequisite> LsQualCardPrerequisites { get; set; } = new List<LsQualCardPrerequisite>();

    public virtual ICollection<LsQualCardRoute> LsQualCardRoutes { get; set; } = new List<LsQualCardRoute>();

    public virtual ICollection<LsQualCard> LsQualCards { get; set; } = new List<LsQualCard>();

    public virtual ICollection<LsQualJobPosition> LsQualJobPositions { get; set; } = new List<LsQualJobPosition>();

    public virtual ICollection<LsSurvey> LsSurveys { get; set; } = new List<LsSurvey>();

    public virtual ICollection<LsLearningEvent> FkLearningEvents { get; set; } = new List<LsLearningEvent>();

    public virtual ICollection<Project> FkProjects { get; set; } = new List<Project>();
}
