using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class LkTblIlamethod
    {
        public LkTblIlamethod()
        {
            TblSmudcourseDesignDelieveryMethods = new HashSet<TblSmudcourseDesignDelieveryMethod>();
        }

        public int Mid { get; set; }
        public string MethodType { get; set; }
        public string Method { get; set; }

        public virtual ICollection<TblSmudcourseDesignDelieveryMethod> TblSmudcourseDesignDelieveryMethods { get; set; }
    }
}
