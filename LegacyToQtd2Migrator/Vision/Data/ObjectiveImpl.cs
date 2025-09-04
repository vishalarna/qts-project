using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ObjectiveImpl
{
    public decimal Id { get; set; }

    public decimal FkObjective { get; set; }

    public decimal FkProject { get; set; }

    public decimal? FkObjectiveLevel { get; set; }

    public string Text { get; set; }

    public string Topic { get; set; }

    public string UserDefinedId { get; set; }

    public decimal? FkObjectiveClass { get; set; }

    public decimal? TrainingTime { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public string MajorVersionNumber { get; set; }

    public string MinorVersionNumber { get; set; }

    public decimal? FkObjectiveSetting { get; set; }

    public decimal? FkObjectiveMedia { get; set; }

    public string TextAscii { get; set; }

    public decimal? FkObjectiveStatus { get; set; }

    public decimal? PracticeQuestionsOnWeb { get; set; }

    public string VersionComments { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public virtual ICollection<ExamUnitOb> ExamUnitObs { get; set; } = new List<ExamUnitOb>();

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual ObjectiveClass FkObjectiveClassNavigation { get; set; }

    public virtual ObjectiveLevel FkObjectiveLevelNavigation { get; set; }

    public virtual ObjectiveMedium FkObjectiveMediaNavigation { get; set; }

    public virtual Objective FkObjectiveNavigation { get; set; }

    public virtual ObjectiveSetting FkObjectiveSettingNavigation { get; set; }

    public virtual ObjectiveStatus FkObjectiveStatusNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }

    public virtual ICollection<RevisionLogObjective> RevisionLogObjectives { get; set; } = new List<RevisionLogObjective>();
}
