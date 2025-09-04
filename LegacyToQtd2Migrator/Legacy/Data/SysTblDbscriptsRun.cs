using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class SysTblDbscriptsRun
    {
        public int Scrid { get; set; }
        public string Product { get; set; }
        public string ScriptVersion { get; set; }
        public DateTime DateRun { get; set; }
        public string RunBy { get; set; }
    }
}
