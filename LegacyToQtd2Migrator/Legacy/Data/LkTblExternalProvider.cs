using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LkTblExternalProvider
    {
        public int Extpid { get; set; }
        public string ExtPname { get; set; }
        public string ExtPuserName { get; set; }
        public string ExtPpassword { get; set; }
        public string ExtPurl { get; set; }
    }
}
