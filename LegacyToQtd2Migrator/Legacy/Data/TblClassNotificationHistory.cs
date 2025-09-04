using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblClassNotificationHistory
    {
        public int Cnhid { get; set; }
        public DateTime Cndate { get; set; }
        public string Cnsender { get; set; }
    }
}
