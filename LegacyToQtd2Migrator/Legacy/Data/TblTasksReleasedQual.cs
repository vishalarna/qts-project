using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTasksReleasedQual
    {
        public int Trqid { get; set; }
        public int Eid { get; set; }
        public int Qid { get; set; }
        public int Tid { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime? QualDate { get; set; }
        public bool? Qstatus { get; set; }
    }
}
