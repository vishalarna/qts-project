using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTrainingModuleResource
    {
        public int Tmrid { get; set; }
        public int Tmid { get; set; }
        public string TmfilePath { get; set; }
        public string TmfileName { get; set; }
    }
}
