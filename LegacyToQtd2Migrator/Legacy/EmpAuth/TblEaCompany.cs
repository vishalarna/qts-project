using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.EmpAuth
{
    public partial class TblEaCompany
    {
        public TblEaCompany()
        {
            TblEaUsers = new HashSet<TblEaUser>();
        }

        public int CompanyId { get; set; }
        public string Company { get; set; }
        public string Dbname { get; set; }
        public string Dbserver { get; set; }
        public string QtdscormServer { get; set; }
        public string ScormServer { get; set; }
        public string CnStr { get; set; }

        public virtual ICollection<TblEaUser> TblEaUsers { get; set; }
    }
}
