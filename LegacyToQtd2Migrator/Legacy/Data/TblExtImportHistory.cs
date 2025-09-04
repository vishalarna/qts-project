using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblExtImportHistory
    {
        public int Impid { get; set; }
        public int Extpid { get; set; }
        public string UserName { get; set; }
        public DateTime ImportDate { get; set; }
        public string ImportStatus { get; set; }
    }
}
