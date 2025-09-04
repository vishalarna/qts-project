using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTargetAudience
    {
        public TblTargetAudience()
        {
            TblSmudcourseDesignTargetAudiences = new HashSet<TblSmudcourseDesignTargetAudience>();
        }

        public int TargetAudienceId { get; set; }
        public string TargetAudience { get; set; }

        public virtual ICollection<TblSmudcourseDesignTargetAudience> TblSmudcourseDesignTargetAudiences { get; set; }
    }
}
