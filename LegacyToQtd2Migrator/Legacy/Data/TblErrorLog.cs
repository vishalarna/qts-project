using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblErrorLog
    {
        public int ErrorLogId { get; set; }
        public string ErrorDesc { get; set; }
        public string FunctionName { get; set; }
        public string ModuleName { get; set; }
        public string UserName { get; set; }
        public DateTime? ErrorDate { get; set; }
        public string ErrorNum { get; set; }
    }
}
