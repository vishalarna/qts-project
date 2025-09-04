using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class IDPSchedule : Common.Entity
    {
        public int IDPId { get; set; }
        
        public int ClassScheduleId { get; set; }
        public virtual IDP IDP { get; set; }

        public virtual ClassSchedule ClassSchedule { get; set; }
        
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public DateTime? plannedDate { get; set; }
        public string? Grade { get; set; }
        public string? GradeReason { get; set; }
        public string? Score { get; set; }
        public bool? TaskQualificationCompleted { get; set; }
        public DateTime? CompletionDate { get; set; }
        public IDPSchedule()
        {

        }

        public IDPSchedule(int iDPId, int classScheduleId, DateTime startDate, DateTime endDate, DateTime? plannedDate, string? grade, string? gradeReason, string? score, bool? taskQualificationCompleted, DateTime? completionDate)
        {
            IDPId = iDPId;
            ClassScheduleId = classScheduleId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.plannedDate = plannedDate;
            Grade = grade;
            GradeReason = gradeReason;
            Score = score;
            CompletionDate = completionDate;
            TaskQualificationCompleted = taskQualificationCompleted;
        }

        public IDPSchedule(int iDPId, int classScheduleId, DateTime startDate, DateTime endDate, DateTime? plannedDate)
        {
            IDPId = iDPId;
            ClassScheduleId = classScheduleId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.plannedDate = plannedDate;
        }

        public void UpdateGradeRelatedData(string? grade, string? gradeReason, string? score, bool? taskQualificationCompleted, DateTime? completionDate)
        {
            Grade = grade;
            GradeReason = gradeReason;
            Score = score;
            TaskQualificationCompleted = taskQualificationCompleted;
            CompletionDate = completionDate;
        }
    }
}
