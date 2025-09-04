using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LktblRatingScale
    {
        public LktblRatingScale()
        {
            TblForms = new HashSet<TblForm>();
            TblGapProjects = new HashSet<TblGapProject>();
        }

        public int Rsid { get; set; }
        public string Rsdescription { get; set; }
        public string Rsinstruction { get; set; }
        public byte[] Ts { get; set; }

        public virtual ICollection<TblForm> TblForms { get; set; }
        public virtual ICollection<TblGapProject> TblGapProjects { get; set; }
    }
}
