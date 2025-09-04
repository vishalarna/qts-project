using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblCertificationHistory
    {
        public int Chid { get; set; }
        public int? Eid { get; set; }
        public string ChNerccertNum { get; set; }
        public int? ChNerccertArea { get; set; }
        public DateTime? ChNerccertIssueDate { get; set; }
        public DateTime? ChNerccertExpDate { get; set; }
        public DateTime? ChDra { get; set; }
        public string ChRaby { get; set; }
        public byte[] Ts { get; set; }
        public DateTime? ChIssueDate { get; set; }

        public virtual TblEmployee EidNavigation { get; set; }
    }
}
