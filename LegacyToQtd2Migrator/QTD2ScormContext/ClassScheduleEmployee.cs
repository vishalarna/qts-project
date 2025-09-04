using System;
using System.Collections.Generic;

#nullable disable

namespace LegacyToQtd2Migrator.QTD2ScormContext
{
    public partial class ClassScheduleEmployee
    {
        public int Id { get; set; }
        public int ClassScheduleId { get; set; }
        public int PreTestStatusId { get; set; }
        public int TestStatusId { get; set; }
        public int RetakeStatusId { get; set; }
        public int CbtstatusId { get; set; }
        public int FinalScore { get; set; }
        public string FinalGrade { get; set; }
        public string GradeNotes { get; set; }
        public int EmployeeId { get; set; }
        public bool IsEnrolled { get; set; }
        public bool IsWaitlisted { get; set; }
        public bool IsDropped { get; set; }
        public bool IsDenied { get; set; }
        public int? TestId { get; set; }
        public bool? Deleted { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? EnrolledDate { get; set; }
        public DateTime? PlannedDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        public virtual ClassSchedule ClassSchedule { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual CbtScormRegistration CbtScormRegistration { get; set; }
    }
}
