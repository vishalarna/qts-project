using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TinCanActorProperty
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public byte[] KeyValHash { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public byte[] ActorId { get; set; }
    }
}
