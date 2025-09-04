using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTasksIntroduction
    {
        public int Tiid { get; set; }
        public int? TypeId { get; set; }
        public int Tid { get; set; }
        public string Description { get; set; }

        public virtual TblTask TidNavigation { get; set; }
        public virtual TblTaskIntroductionType Type { get; set; }
    }
}
