using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblQtsmgrOrganization
    {
        public int Eid { get; set; }
        public int OrgId { get; set; }
        public string AddedBy { get; set; }
        public DateTime DateAdded { get; set; }

        public virtual TblEmployee EidNavigation { get; set; }
        public virtual LktblOrganization Org { get; set; }
    }
}
