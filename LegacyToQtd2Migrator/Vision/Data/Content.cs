using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Content
{
    public decimal Id { get; set; }

    public virtual ICollection<ContentImpl> ContentImpls { get; set; } = new List<ContentImpl>();

    public virtual ICollection<ContentStorageAicc> ContentStorageAiccs { get; set; } = new List<ContentStorageAicc>();

    public virtual ICollection<ContentStorage> ContentStorages { get; set; } = new List<ContentStorage>();

    public virtual ICollection<LsDocument> LsDocuments { get; set; } = new List<LsDocument>();

    public virtual ICollection<LsHoldRelease> LsHoldReleases { get; set; } = new List<LsHoldRelease>();

    public virtual ICollection<LsObjectiveTrack> LsObjectiveTracks { get; set; } = new List<LsObjectiveTrack>();

    public virtual OfficeMigration OfficeMigration { get; set; }

    public virtual ICollection<ProgramObjectiveContent> ProgramObjectiveContents { get; set; } = new List<ProgramObjectiveContent>();
}
