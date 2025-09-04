using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblOjtevaluator
    {
        public int Tqid { get; set; }
        public int Eid { get; set; }
        public int Tid { get; set; }
        public int EvalEid { get; set; }
        public int? Ojthid { get; set; }
        public bool? HasSigned { get; set; }
        public string Comments { get; set; }
        public string EvalMethod { get; set; }
        public DateTime? SignedDate { get; set; }

        public virtual TblEmployee EidNavigation { get; set; }
        public virtual TblEmployee EvalE { get; set; }
        public virtual TblTask TidNavigation { get; set; }
    }
}
