using System;
using System.Collections.Generic;

namespace QTD2.Infrastructure.Model.TrainingProgramReview
{
    public class TrainingProgramReview_Employee_Link_ViewModel
    {
        public int Id { get; set; }
        public int TrainingProgramReviewId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeePersonFullName { get; set; }
    }
}
