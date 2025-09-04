using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.EmpAuth
{
    public partial class TblEaUser
    {
        public int RhuserId { get; set; }
        public int CompanyId { get; set; }
        public string Email { get; set; }
        public byte[] Pwd { get; set; }
        public string Uname { get; set; }
        public DateTime? LockOut { get; set; }
        public string Company { get; set; }
        public int? Eid { get; set; }
        public string Role { get; set; }
        public int LockoutAttempts { get; set; }

        public virtual TblEaCompany CompanyNavigation { get; set; }
    }
}
