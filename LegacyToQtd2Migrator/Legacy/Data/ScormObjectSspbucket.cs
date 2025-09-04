using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class ScormObjectSspbucket
    {
        public int ScormObjectId { get; set; }
        public int BucketIndex { get; set; }
        public string BucketIdentifier { get; set; }
        public string BucketType { get; set; }
        public int Persistence { get; set; }
        public long SizeMin { get; set; }
        public long SizeRequested { get; set; }
        public bool Reducible { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }

        public virtual ScormObject ScormObject { get; set; }
    }
}
