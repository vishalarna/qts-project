using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblOjthistoryStep
    {
        public int Tqsid { get; set; }
        public int Ojthid { get; set; }
        public int Eid { get; set; }
        public int TidStep { get; set; }
        public string StepNum { get; set; }
        public string StepDesc { get; set; }
        public int EvalEid { get; set; }
        public DateTime? StepEvalDate { get; set; }
        public bool IsQualified { get; set; }
        public string Comment { get; set; }

        public virtual TblEmployee EidNavigation { get; set; }
        public virtual TblEmployee EvalE { get; set; }
        public virtual TblOjthistory Ojth { get; set; }
        public virtual TblTask TidStepNavigation { get; set; }
    }
}
