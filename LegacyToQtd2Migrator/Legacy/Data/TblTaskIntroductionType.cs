using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTaskIntroductionType
    {
        public TblTaskIntroductionType()
        {
            TblTasksIntroductions = new HashSet<TblTasksIntroduction>();
        }

        public int IntroTypeId { get; set; }
        public string IntroTypeText { get; set; }

        public virtual ICollection<TblTasksIntroduction> TblTasksIntroductions { get; set; }
    }
}
