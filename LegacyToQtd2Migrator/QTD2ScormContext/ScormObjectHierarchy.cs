using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ScormObjectHierarchy
    {
        public string UpdateBy { get; set; }
        public DateTime UpdateDt { get; set; }
        public short EngineTenantId { get; set; }
        public int ParentScormObjectId { get; set; }
        public int ChildScormObjectId { get; set; }
        public int Ordinal { get; set; }

        public virtual ScormObject ScormObject { get; set; }
        public virtual ScormObject ScormObjectNavigation { get; set; }
    }
}
