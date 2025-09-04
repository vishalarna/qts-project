using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_Evaluation_Roster : Entity
    {
        public virtual MetaILA MetaILA { get; set; }
        public virtual ClassSchedule ClassScheduleInfo { get; set; }
        public virtual Employee Employee{ get; set; }
        public virtual StudentEvaluation StudentEvaluationInfo{ get; set; }

        public DateTime? ReleaseDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int? ClassScheduleId { get; set; }
        public int? MetaIlaId { get; set; }
        public int StudentEvaluationId { get; set; }
        public int EmployeeId { get; set; }

        public bool IsCompleted { get; set; }
        public bool IsStarted { get; set; }
        public bool IsAllowed { get; set; }

        public bool IsFromClassSchedule { get { return ClassScheduleId.HasValue; } }
        public bool IsFromMetaILA { get { return MetaIlaId.HasValue; } }

        public int ParentId { get { return IsFromClassSchedule ? ClassScheduleId.Value : (IsFromMetaILA ? MetaIlaId.Value : -1); } }

        public string  getStatus()
        {
            if (this.IsCompleted)
            {
                return "Completed";
            }
            else if(this.IsStarted && !this.IsCompleted)
            {
                return "In Progress";
            }
            else if(!this.IsStarted && !this.IsCompleted)
            {
                return "Pending";
            }
            return "Invalid";

        }
        public bool IsReleased { get; set; }
        public bool IsRecalled { get; set; }

        public ClassSchedule_Evaluation_Roster()
        {

        }
         public ClassSchedule_Evaluation_Roster(DateTime? releaseDate, DateTime? completedDate, int classScheduleId, int employeeId, bool isCompleted, bool isReleased,bool isStarted,int studentEvaluationId)
         {
            this.ReleaseDate = releaseDate;
            this.CompletedDate = completedDate;
            this.ClassScheduleId = classScheduleId;
            this.EmployeeId = employeeId;
            this.IsCompleted = isCompleted;
            this.IsStarted = isStarted;
            this.IsReleased = isReleased;
            this.StudentEvaluationId = studentEvaluationId;
            IsAllowed = true;
        }

        public ClassSchedule_Evaluation_Roster( int classScheduleId, int employeeId, int studentEvaluationId,bool isAllowed)
        {
            this.ClassScheduleId = classScheduleId;
            this.EmployeeId = employeeId;
            this.StudentEvaluationId = studentEvaluationId;
            this.IsAllowed = isAllowed;
        }

        public ClassSchedule_Evaluation_Roster(int metaIlaId, int employeeId, int studentEvaluationId)
        {
            this.MetaIlaId = metaIlaId;
            this.EmployeeId = employeeId;
            this.StudentEvaluationId = studentEvaluationId;
            this.IsAllowed = true;
        }

        public void Release(DateTime releaseDate)
        {
            IsReleased = true;
            ReleaseDate = releaseDate;

            AddDomainEvent(new Events.Core.OnClassSchedule_StudentEvaluations_StudentEvaluationReleased(this));
        }

        public void SetIsCompleted(bool isCompleted)
        {
           IsCompleted = isCompleted;
        }

        public void SetIsStarted(bool isStarted)
        {
            IsStarted = isStarted;
        }
        public void SetIsReleased(bool isReleased)
        {
            IsReleased = isReleased;
        }
        public void SetIsAllowed(bool isAllowed)
        {
            IsAllowed = isAllowed;
        }

        public void SetReleaseDate(DateTime? releaseDate)
        {
            ReleaseDate = releaseDate;
        }

        public void SetCompletedDate(DateTime? completedDate)
        {
            CompletedDate = completedDate;
        }

    }

}
