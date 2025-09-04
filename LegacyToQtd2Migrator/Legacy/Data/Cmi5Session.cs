using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class Cmi5Session
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public Guid Cmi5SessionId { get; set; }
        public int ScormRegistrationId { get; set; }
        public int ScormObjectId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastRequestTime { get; set; }
        public string LaunchMode { get; set; }
        public byte[] LaunchTokenId { get; set; }
        public bool LaunchTokenFetched { get; set; }
        public bool IsLaunched { get; set; }
        public bool IsInitialized { get; set; }
        public bool IsTerminated { get; set; }
        public bool IsFailed { get; set; }
        public long TimeReported { get; set; }
        public int? LaunchHistoryId { get; set; }

        public virtual Cmi5RegistrationToAu Cmi5RegistrationToAu { get; set; }
    }
}
