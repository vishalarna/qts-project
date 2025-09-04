using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.EmpAuth
{
    public partial class TblEaEventList
    {
        public TblEaEventList()
        {
            TblEaAuthlogs = new HashSet<TblEaAuthlog>();
        }

        public int EventCode { get; set; }
        public string EventType { get; set; }
        public string EventDetails { get; set; }

        public virtual ICollection<TblEaAuthlog> TblEaAuthlogs { get; set; }
    }
}
