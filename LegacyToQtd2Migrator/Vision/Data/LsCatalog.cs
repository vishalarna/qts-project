using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class LsCatalog
{
    public decimal Id { get; set; }

    public decimal? FkCompany { get; set; }

    public decimal? FkStructure { get; set; }

    public string Text { get; set; }

    public decimal Type { get; set; }

    public decimal Status { get; set; }

    public string Description { get; set; }

    public string CrossReference { get; set; }

    public string Userdefined1 { get; set; }

    public string Userdefined2 { get; set; }

    public decimal? Frequency { get; set; }

    public decimal? FkProgram { get; set; }

    public decimal FkCourseType { get; set; }

    public decimal? RequalPeriod { get; set; }

    public decimal? RequalModifier { get; set; }

    public decimal FkOriginalCatalogId { get; set; }

    public decimal Approved { get; set; }

    public decimal? SimHours { get; set; }

    public decimal? StandardHours { get; set; }

    public decimal? TotalHours { get; set; }

    public decimal? EmergencyHours { get; set; }

    public decimal FkTrainingType { get; set; }

    public decimal RequiredCourse { get; set; }

    public virtual LsCompany FkCompanyNavigation { get; set; }

    public virtual LsCourseType FkCourseTypeNavigation { get; set; }

    public virtual LsCatalog FkOriginalCatalog { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual LsOrg FkStructureNavigation { get; set; }

    public virtual LsTrainingType FkTrainingTypeNavigation { get; set; }

    public virtual LsTimeToComplete FrequencyNavigation { get; set; }

    public virtual ICollection<LsCatalog> InverseFkOriginalCatalog { get; set; } = new List<LsCatalog>();

    public virtual ICollection<LsCatalogLesson> LsCatalogLessons { get; set; } = new List<LsCatalogLesson>();

    public virtual ICollection<LsCatalogPrereq> LsCatalogPrereqs { get; set; } = new List<LsCatalogPrereq>();

    public virtual ICollection<LsLearningEvent> LsLearningEvents { get; set; } = new List<LsLearningEvent>();

    public virtual ICollection<LsProgramCompletion> LsProgramCompletions { get; set; } = new List<LsProgramCompletion>();

    public virtual LsDateModifier RequalModifierNavigation { get; set; }

    public virtual LsTimeToComplete RequalPeriodNavigation { get; set; }

    public virtual LsCatalogType TypeNavigation { get; set; }
}
