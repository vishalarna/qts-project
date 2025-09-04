using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormObject
    {
        public ScormObject()
        {
            Cmi5ObjectiveMapScormObjectNavigations = new HashSet<Cmi5ObjectiveMap>();
            Cmi5ObjectiveMapScormObjects = new HashSet<Cmi5ObjectiveMap>();
            Cmi5RegistrationToAus = new HashSet<Cmi5RegistrationToAu>();
            ScormActivities = new HashSet<ScormActivity>();
            ScormMetadata = new HashSet<ScormMetadatum>();
            ScormObjectHierarchyScormObjectNavigations = new HashSet<ScormObjectHierarchy>();
            ScormObjectHierarchyScormObjects = new HashSet<ScormObjectHierarchy>();
            ScormObjectSharedDataMaps = new HashSet<ScormObjectSharedDataMap>();
            ScormObjectSspbuckets = new HashSet<ScormObjectSspbucket>();
        }

        public int ScormObjectId { get; set; }
        public int ScormPackageId { get; set; }
        public int ScormObjectTypeId { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public string Parameters { get; set; }
        public string DataFromLms { get; set; }
        public decimal? MasteryScore { get; set; }
        public long? MaxTimeAllowed { get; set; }
        public int? TimeLimitAction { get; set; }
        public string Prerequisites { get; set; }
        public bool Visible { get; set; }
        public decimal? CompletionThreshold { get; set; }
        public bool PersistState { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public string FileList { get; set; }
        public bool CompletedByMeasure { get; set; }
        public decimal CompletionProgressWeight { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormPackage ScormPackage { get; set; }
        public virtual Cmi5AuLaunchDatum Cmi5AuLaunchDatum { get; set; }
        public virtual ScormObjectIdentifier ScormObjectIdentifier { get; set; }
        public virtual ScormObjectSeqDatum ScormObjectSeqDatum { get; set; }
        public virtual ICollection<Cmi5ObjectiveMap> Cmi5ObjectiveMapScormObjectNavigations { get; set; }
        public virtual ICollection<Cmi5ObjectiveMap> Cmi5ObjectiveMapScormObjects { get; set; }
        public virtual ICollection<Cmi5RegistrationToAu> Cmi5RegistrationToAus { get; set; }
        public virtual ICollection<ScormActivity> ScormActivities { get; set; }
        public virtual ICollection<ScormMetadatum> ScormMetadata { get; set; }
        public virtual ICollection<ScormObjectHierarchy> ScormObjectHierarchyScormObjectNavigations { get; set; }
        public virtual ICollection<ScormObjectHierarchy> ScormObjectHierarchyScormObjects { get; set; }
        public virtual ICollection<ScormObjectSharedDataMap> ScormObjectSharedDataMaps { get; set; }
        public virtual ICollection<ScormObjectSspbucket> ScormObjectSspbuckets { get; set; }
    }
}
