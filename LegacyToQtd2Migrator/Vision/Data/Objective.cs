using System;
using System.Collections.Generic;

namespace LegacyToQtd2Migrator.Vision.Data;

public partial class Objective
{
    public decimal Id { get; set; }

    public virtual ICollection<Consolidation> Consolidations { get; set; } = new List<Consolidation>();

    public virtual ICollection<DirectObjective> DirectObjectives { get; set; } = new List<DirectObjective>();

    public virtual ICollection<ExamUnitOb> ExamUnitObs { get; set; } = new List<ExamUnitOb>();

    public virtual ICollection<LsDocument> LsDocuments { get; set; } = new List<LsDocument>();

    public virtual ICollection<LsHoldRelease> LsHoldReleases { get; set; } = new List<LsHoldRelease>();

    public virtual ICollection<LsObjExternalTrack> LsObjExternalTracks { get; set; } = new List<LsObjExternalTrack>();

    public virtual ICollection<LsObjectiveMastery> LsObjectiveMasteries { get; set; } = new List<LsObjectiveMastery>();

    public virtual ICollection<LsObjectiveTrack> LsObjectiveTracks { get; set; } = new List<LsObjectiveTrack>();

    public virtual ICollection<LsOnlineExamQuestion> LsOnlineExamQuestions { get; set; } = new List<LsOnlineExamQuestion>();

    public virtual ICollection<LsVLessonOb> LsVLessonObs { get; set; } = new List<LsVLessonOb>();

    public virtual ICollection<ObjectiveComment> ObjectiveComments { get; set; } = new List<ObjectiveComment>();

    public virtual ICollection<ObjectiveCondStand> ObjectiveCondStands { get; set; } = new List<ObjectiveCondStand>();

    public virtual ICollection<ObjectiveHierarchy> ObjectiveHierarchyFkChildNavigations { get; set; } = new List<ObjectiveHierarchy>();

    public virtual ICollection<ObjectiveHierarchy> ObjectiveHierarchyFkParentNavigations { get; set; } = new List<ObjectiveHierarchy>();

    public virtual ICollection<ObjectiveHtml> ObjectiveHtmls { get; set; } = new List<ObjectiveHtml>();

    public virtual ICollection<ObjectiveImpl> ObjectiveImpls { get; set; } = new List<ObjectiveImpl>();

    public virtual ICollection<ObjectiveQuestion> ObjectiveQuestions { get; set; } = new List<ObjectiveQuestion>();

    public virtual ICollection<ObjectiveTask> ObjectiveTasks { get; set; } = new List<ObjectiveTask>();

    public virtual ICollection<ProgramObjectiveContent> ProgramObjectiveContents { get; set; } = new List<ProgramObjectiveContent>();

    public virtual ICollection<RevisionLogObjective> RevisionLogObjectives { get; set; } = new List<RevisionLogObjective>();

    public virtual ICollection<Sequencing> Sequencings { get; set; } = new List<Sequencing>();
}
