using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.Legacy.Data
{
    public partial class TblTest
    {
        public TblTest()
        {
            RsTblTestTestItems = new HashSet<RsTblTestTestItem>();
            TblSmudcourseImplementClassScheduleRetakeTests = new HashSet<TblSmudcourseImplementClassSchedule>();
            TblSmudcourseImplementClassScheduleTests = new HashSet<TblSmudcourseImplementClassSchedule>();
            TblTdtrandomReviewDetails = new HashSet<TblTdtrandomReviewDetail>();
            TblTdtrandomReviews = new HashSet<TblTdtrandomReview>();
        }

        public int TestId { get; set; }
        public int? Corid { get; set; }
        public string Notes { get; set; }
        public byte? TestStatusId { get; set; }
        public byte[] Ts { get; set; }
        public string TestTitle { get; set; }

        public virtual TblCourse Cor { get; set; }
        public virtual TblTestStatus TestStatus { get; set; }
        public virtual ICollection<RsTblTestTestItem> RsTblTestTestItems { get; set; }
        public virtual ICollection<TblSmudcourseImplementClassSchedule> TblSmudcourseImplementClassScheduleRetakeTests { get; set; }
        public virtual ICollection<TblSmudcourseImplementClassSchedule> TblSmudcourseImplementClassScheduleTests { get; set; }
        public virtual ICollection<TblTdtrandomReviewDetail> TblTdtrandomReviewDetails { get; set; }
        public virtual ICollection<TblTdtrandomReview> TblTdtrandomReviews { get; set; }
    }
}
