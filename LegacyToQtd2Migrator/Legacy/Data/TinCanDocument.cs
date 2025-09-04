using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanDocument
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int? Version { get; set; }
        public string RegistrationId { get; set; }
        public byte[] ActorId { get; set; }
        public string ActivityId { get; set; }
        public string DocumentId { get; set; }
        public byte[] DocumentIdHash { get; set; }
        public byte[] DocumentCtxHash { get; set; }
        public long Updated { get; set; }
        public long ContentLength { get; set; }
        public string ContentType { get; set; }
        public byte[] ContentHash { get; set; }
        public string AsserterId { get; set; }
        public string AsserterJson { get; set; }
    }
}
