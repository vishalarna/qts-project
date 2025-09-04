using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblOjthistory
    {
        public TblOjthistory()
        {
            TblOjthistoryQuestions = new HashSet<TblOjthistoryQuestion>();
            TblOjthistorySteps = new HashSet<TblOjthistoryStep>();
        }

        public int Ojthid { get; set; }
        public int? ParentId { get; set; }
        public bool Complete { get; set; }
        public string Comments { get; set; }
        public DateTime? EvalDate { get; set; }
        public string Evaluator { get; set; }
        public string ObservPeriod { get; set; }
        public string EvalMethod { get; set; }
        public int? EvalId { get; set; }
        public int? Clid { get; set; }
        public byte[] Ts { get; set; }
        public string Tdesc { get; set; }
        public string Tnum { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? EvalEid { get; set; }
        public DateTime? OnlineSubmissionDate { get; set; }
        public bool HasTraineeSigned { get; set; }

        public virtual ICollection<TblOjthistoryQuestion> TblOjthistoryQuestions { get; set; }
        public virtual ICollection<TblOjthistoryStep> TblOjthistorySteps { get; set; }
    }
}
