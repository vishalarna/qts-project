using System;
using System.Collections.Generic;
using System.Linq;

namespace QTD2.Domain.Entities.Core
{
    public class TrainingProgramReview_Employee_Link : Common.Entity
    {
        public int TrainingProgramReviewId { get; set; }
        public virtual TrainingProgramReview TrainingProgramReview { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public TrainingProgramReview_Employee_Link()
        {
        }

        public TrainingProgramReview_Employee_Link(int trainingProgramReviewId, int employeeId)
        {
            TrainingProgramReviewId = trainingProgramReviewId;
            EmployeeId = employeeId;
        }
    }
}
