using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblIdpreview
    {
        public int Idpid { get; set; }
        public string IdpTitle { get; set; }
        public string IdpReleaseText { get; set; }
        public DateTime? IdpStartDate { get; set; }
        public DateTime? IdpEndDate { get; set; }
        public bool? IdpStatus { get; set; }
        public DateTime? IdpCreatedDate { get; set; }
        public string IdpCreatedBy { get; set; }
        public string IdpReleaseTextPlain { get; set; }
    }
}
