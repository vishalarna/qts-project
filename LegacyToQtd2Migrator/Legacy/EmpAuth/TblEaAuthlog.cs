using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.EmpAuth
{
    public partial class TblEaAuthlog
    {
        public int AuthEventId { get; set; }
        public int EventCode { get; set; }
        public DateTime AuthEventTime { get; set; }
        public string UserIpaddress { get; set; }
        public int RhuserId { get; set; }
        public int CompanyId { get; set; }
        public string Email { get; set; }
        public string AuthEventNotes { get; set; }

        public virtual TblEaEventList EventCodeNavigation { get; set; }
    }
}
