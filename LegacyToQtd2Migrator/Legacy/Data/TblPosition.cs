using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblPosition
    {
        public TblPosition()
        {
            RstblPositionsTasks = new HashSet<RstblPositionsTask>();
            TblDifsurveyEmployees = new HashSet<TblDifsurveyEmployee>();
            TblGapRatings = new HashSet<TblGapRating>();
            TblGapstatuses = new HashSet<TblGapstatus>();
            TblPosTaskAnnualReviews = new HashSet<TblPosTaskAnnualReview>();
            TblTrainingPhases = new HashSet<TblTrainingPhase>();
        }

        public int Pid { get; set; }
        public int? Pnum { get; set; }
        public string Pabbrev { get; set; }
        public string Pdesc { get; set; }
        public byte[] Ts { get; set; }
        public string Pdescription { get; set; }

        public virtual ICollection<RstblPositionsTask> RstblPositionsTasks { get; set; }
        public virtual ICollection<TblDifsurveyEmployee> TblDifsurveyEmployees { get; set; }
        public virtual ICollection<TblGapRating> TblGapRatings { get; set; }
        public virtual ICollection<TblGapstatus> TblGapstatuses { get; set; }
        public virtual ICollection<TblPosTaskAnnualReview> TblPosTaskAnnualReviews { get; set; }
        public virtual ICollection<TblTrainingPhase> TblTrainingPhases { get; set; }
    }
}
