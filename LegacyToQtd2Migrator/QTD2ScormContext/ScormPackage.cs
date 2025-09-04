using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormPackage
    {
        public ScormPackage()
        {
            ScormMetadata = new HashSet<ScormMetadatum>();
            ScormObjects = new HashSet<ScormObject>();
            ScormRegistrations = new HashSet<ScormRegistration>();
        }

        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormPackageId { get; set; }
        public string ApiCourseId { get; set; }
        public bool ObjectivesGlobalToSystem { get; set; }
        public byte[] ContentConnectorId { get; set; }
        public string ConnectorContentId { get; set; }
        public int LearningStandardId { get; set; }
        public string WebPath { get; set; }
        public int VersionId { get; set; }
        public bool? SharedDataGlobalToSystem { get; set; }
        public string DisplayTitle { get; set; }
        public string InvariantTitle { get; set; }

        public virtual ICollection<ScormMetadatum> ScormMetadata { get; set; }
        public virtual ICollection<ScormObject> ScormObjects { get; set; }
        public virtual ICollection<ScormRegistration> ScormRegistrations { get; set; }
    }
}
