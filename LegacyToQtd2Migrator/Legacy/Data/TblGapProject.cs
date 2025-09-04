using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblGapProject
    {
        public TblGapProject()
        {
            TblGapRatings = new HashSet<TblGapRating>();
            TblGapstatuses = new HashSet<TblGapstatus>();
        }

        public int Prjid { get; set; }
        public string Prjtitle { get; set; }
        public int Rsid { get; set; }
        public DateTime Prjdate { get; set; }
        public int EntryFormat { get; set; }
        public DateTime? EndDate { get; set; }
        public string SurveyDetails { get; set; }

        public virtual LktblRatingScale Rs { get; set; }
        public virtual ICollection<TblGapRating> TblGapRatings { get; set; }
        public virtual ICollection<TblGapstatus> TblGapstatuses { get; set; }
    }
}
