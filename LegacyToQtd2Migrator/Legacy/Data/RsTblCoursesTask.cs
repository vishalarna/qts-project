using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class RsTblCoursesTask
    {
        public int Tid { get; set; }
        public int Corid { get; set; }
        public byte[] Ts { get; set; }
        public int? Sequence { get; set; }

        public virtual TblCourse Cor { get; set; }
        public virtual TblTask TidNavigation { get; set; }
    }
}
