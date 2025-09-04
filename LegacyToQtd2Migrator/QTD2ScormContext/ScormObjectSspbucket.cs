using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormObjectSspbucket
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ScormObjectId { get; set; }
        public int BucketIndex { get; set; }
        public string BucketIdentifier { get; set; }
        public string BucketType { get; set; }
        public int Persistence { get; set; }
        public long SizeMin { get; set; }
        public long SizeRequested { get; set; }
        public bool Reducible { get; set; }

        public virtual ScormObject ScormObject { get; set; }
    }
}
