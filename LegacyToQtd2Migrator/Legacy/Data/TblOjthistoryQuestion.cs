using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblOjthistoryQuestion
    {
        public int Tqqid { get; set; }
        public int? Ojthid { get; set; }
        public int Eid { get; set; }
        public int Tqnumber { get; set; }
        public string Tqquestion { get; set; }
        public string Tqanswer { get; set; }
        public string Comment { get; set; }
        public int EvalEid { get; set; }

        public virtual TblEmployee EidNavigation { get; set; }
        public virtual TblEmployee EvalE { get; set; }
        public virtual TblOjthistory Ojth { get; set; }
    }
}
