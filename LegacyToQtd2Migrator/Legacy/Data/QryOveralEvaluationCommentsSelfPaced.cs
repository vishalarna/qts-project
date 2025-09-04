using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class QryOveralEvaluationCommentsSelfPaced
    {
        public int? SelfPacedCorid { get; set; }
        public int? Fid { get; set; }
        public int? Fqid { get; set; }
        public float? Fqnum { get; set; }
        public string Fqdesc { get; set; }
        public string Sfacomments { get; set; }
        public DateTime? EvalDate { get; set; }
    }
}
