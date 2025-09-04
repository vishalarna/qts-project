using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTasksQuestion
    {
        public int Tqid { get; set; }
        public int Tid { get; set; }
        public int? Tqnumber { get; set; }
        public string Tqquestion { get; set; }
        public string Tqanswer { get; set; }

        public virtual TblTask TidNavigation { get; set; }
    }
}
