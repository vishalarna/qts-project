using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormMetadatum
    {
        public int ScormMetadataId { get; set; }
        public int ScormPackageId { get; set; }
        public int? ScormObjectId { get; set; }
        public int MetadataIndex { get; set; }
        public string MetadataXml { get; set; }
        public string Identifier { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string LanguageCode { get; set; }
        public string Version { get; set; }
        public string Duration { get; set; }
        public string TypicalLearningTime { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public string FileHref { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormObject ScormObject { get; set; }
        public virtual ScormPackage ScormPackage { get; set; }
    }
}
