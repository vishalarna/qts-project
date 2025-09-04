using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblDifproject
    {
        public TblDifproject()
        {
            TblDifsurveyEmployees = new HashSet<TblDifsurveyEmployee>();
        }

        public int Difprjid { get; set; }
        public string Difprjtitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Explanation { get; set; }
        public int? Difstatus { get; set; }
        public bool? HistoricalOnly { get; set; }
        public int? Pid { get; set; }

        public virtual ICollection<TblDifsurveyEmployee> TblDifsurveyEmployees { get; set; }
    }
}
