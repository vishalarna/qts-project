using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblClassNotificationEmployee
    {
        public int Eid { get; set; }

        public virtual TblEmployee EidNavigation { get; set; }
    }
}
