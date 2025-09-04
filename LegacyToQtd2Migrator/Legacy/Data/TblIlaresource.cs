using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblIlaresource
    {
        public int Ilarid { get; set; }
        public int? Corid { get; set; }
        public int? Ilarnum { get; set; }
        public string Ilartitle { get; set; }
        public string Ilarsection { get; set; }
        public string Ilarchapter { get; set; }
        public string Ilarhyperlink { get; set; }
        public string IlarhyperlinkText { get; set; }
        public string Ilarcomments { get; set; }
        public byte[] Ts { get; set; }

        public virtual TblCourse Cor { get; set; }
    }
}
