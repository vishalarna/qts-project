using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTempList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Num { get; set; }
        public int? SubNum { get; set; }
        public bool Selected { get; set; }
        public string Letter { get; set; }
        public string CurrentUser { get; set; }
    }
}
