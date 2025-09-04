using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class ProgramImpl
{
    public decimal Id { get; set; }

    public decimal FkProgram { get; set; }

    public decimal FkProject { get; set; }

    public decimal? FkProgramLevel { get; set; }

    public string Text { get; set; }

    public string UserDefinedId { get; set; }

    public string CrossReference { get; set; }

    public decimal? FkProgramStatus { get; set; }

    public decimal? IsBucket { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal FkCreatedBy { get; set; }

    public string MajorVersionNumber { get; set; }

    public string MinorVersionNumber { get; set; }

    public string TextAscii { get; set; }

    public decimal? IntroWebDisplay { get; set; }

    public decimal? FkProgramTuType { get; set; }

    public decimal? FkProgramOrganizerType { get; set; }

    public string VersionComments { get; set; }

    public decimal? WeightingType { get; set; }

    public DateTime DateExpired { get; set; }

    public decimal? FkExpiredBy { get; set; }

    public decimal? SummaryWebDisplay { get; set; }

    public virtual ICollection<ExamUnitPg> ExamUnitPgs { get; set; } = new List<ExamUnitPg>();

    public virtual Developer FkCreatedByNavigation { get; set; }

    public virtual Developer FkExpiredByNavigation { get; set; }

    public virtual ProgramLevel FkProgramLevelNavigation { get; set; }

    public virtual Program FkProgramNavigation { get; set; }

    public virtual ProgramOrganizerType FkProgramOrganizerTypeNavigation { get; set; }

    public virtual ProgramStatus FkProgramStatusNavigation { get; set; }

    public virtual ProgramTuType FkProgramTuTypeNavigation { get; set; }

    public virtual Project FkProjectNavigation { get; set; }

    public virtual ICollection<LsObjectiveTrack> LsObjectiveTracks { get; set; } = new List<LsObjectiveTrack>();

    public virtual ICollection<RevisionLogProgram> RevisionLogPrograms { get; set; } = new List<RevisionLogProgram>();
}
