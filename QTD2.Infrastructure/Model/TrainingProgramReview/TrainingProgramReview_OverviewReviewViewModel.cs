using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Model.TrainingProgramReview
{
    public class TrainingProgramReview_OverviewReviewViewModel
    {
        public int TrainingProgramReviewId { get; set; }
        public int TrainingProgramId { get; set; }
        public int TrainingProgramTypeId { get; set; }
        public string TrainingProgramType { get; set; }
        public int PositionId { get; set; }
        public string PositionAbbreviation { get; set; }
        public string PositionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TrainingProgramReview_Employee_Link_ViewModel> Reviewers { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool Published { get; set; }
        public bool Active { get; set; }
    }
}
