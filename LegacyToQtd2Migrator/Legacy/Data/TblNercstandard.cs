using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblNercstandard
    {
        public TblNercstandard()
        {
            TblSmudcourseDesignNercs = new HashSet<TblSmudcourseDesignNerc>();
        }

        public int Nsid { get; set; }
        public string Nsname { get; set; }
        public bool? UserDefined { get; set; }

        public virtual ICollection<TblSmudcourseDesignNerc> TblSmudcourseDesignNercs { get; set; }
    }
}
