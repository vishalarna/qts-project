using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblContentObjectPresentation
    {
        public TblContentObjectPresentation()
        {
            TblCopresentationCos = new HashSet<TblCopresentationCo>();
        }

        public int Pid { get; set; }
        public string Title { get; set; }
        public int Corid { get; set; }

        public virtual TblCourse Cor { get; set; }
        public virtual ICollection<TblCopresentationCo> TblCopresentationCos { get; set; }
    }
}
