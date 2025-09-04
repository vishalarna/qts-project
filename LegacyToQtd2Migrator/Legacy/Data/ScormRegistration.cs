using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormRegistration
    {
        public ScormRegistration()
        {
            ScormActivities = new HashSet<ScormActivity>();
            ScormAiccSessions = new HashSet<ScormAiccSession>();
            ScormLaunchHistories = new HashSet<ScormLaunchHistory>();
            ScormRegistrationGlobalObjs = new HashSet<ScormRegistrationGlobalObj>();
            ScormRegistrationSharedData = new HashSet<ScormRegistrationSharedDatum>();
            ScormRegistrationSharedDataVals = new HashSet<ScormRegistrationSharedDataVal>();
            ScormRegistrationSspbuckets = new HashSet<ScormRegistrationSspbucket>();
            ScormRegistrationStatementMaps = new HashSet<ScormRegistrationStatementMap>();
        }

        public int ScormRegistrationId { get; set; }
        public int ScormPackageId { get; set; }
        public string GlobalObjectiveScope { get; set; }
        public int? SuspendedActivityId { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public int InstanceId { get; set; }
        public string ClId { get; set; }
        public string EId { get; set; }
        public bool ConvertedToTincan { get; set; }
        public short EngineTenantId { get; set; }
        public DateTime? UpdateDtUtc { get; set; }
        public bool? CreatedForCredit { get; set; }
        public DateTime? CreateDtUtc { get; set; }
        public DateTime? FirstaccessDtUtc { get; set; }
        public DateTime? CompletedDtUtc { get; set; }
        public byte[] RuntimeData { get; set; }
        public int UpdateSequence { get; set; }

        public virtual ScormPackage ScormPackage { get; set; }
        public virtual TinCanRegistration TinCanRegistration { get; set; }
        public virtual ICollection<ScormActivity> ScormActivities { get; set; }
        public virtual ICollection<ScormAiccSession> ScormAiccSessions { get; set; }
        public virtual ICollection<ScormLaunchHistory> ScormLaunchHistories { get; set; }
        public virtual ICollection<ScormRegistrationGlobalObj> ScormRegistrationGlobalObjs { get; set; }
        public virtual ICollection<ScormRegistrationSharedDatum> ScormRegistrationSharedData { get; set; }
        public virtual ICollection<ScormRegistrationSharedDataVal> ScormRegistrationSharedDataVals { get; set; }
        public virtual ICollection<ScormRegistrationSspbucket> ScormRegistrationSspbuckets { get; set; }
        public virtual ICollection<ScormRegistrationStatementMap> ScormRegistrationStatementMaps { get; set; }
    }
}
