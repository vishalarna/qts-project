using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryStudentFormsClass
    {
        public int Sfid { get; set; }
        public DateTime? Cldate { get; set; }
        public int? Inid { get; set; }
        public int? Corid { get; set; }
        public bool Sfcomplete { get; set; }
        public int? Fid { get; set; }
    }
}
