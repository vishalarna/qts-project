using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_Employee : Common.Entity
    {
        public int ClassScheduleId { get; set; } //search

        public int PreTestStatusId { get; set; }

        public int TestStatusId { get; set; }

        public int RetakeStatusId { get; set; }

        public int CBTStatusId { get; set; }

        public int? FinalScore { get; set; }

        public string FinalGrade { get; set; }
        public string GradeNotes { get; set; }
        public DateTime? EnrolledDate { get; set; }
        public DateTime? PlannedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int EmployeeId { get; set; }

        public bool IsEnrolled { get; set; }

        public bool IsWaitlisted { get; set; }
        public bool IsDropped { get; set; }
        public bool IsDenied { get; set; }
        public bool? IsAwaitingForApproval { get; set; }
        public bool PopulateOJTRecord { get; set; }
        public virtual Employee Employee { get; set; }

        public virtual ClassSchedule ClassSchedule { get; set; }

        public virtual ClassSchedule_Roster_Statuses PreTestStatus { get; set; }

        public virtual ClassSchedule_Roster_Statuses CBTStatus { get; set; }

        public ClassSchedule_Roster_Statuses TestStatus { get; set; }

        public ClassSchedule_Roster_Statuses ReTakeStatus { get; set; }

        public virtual ICollection<CBT_ScormRegistration> ScormRegistrations { get; set; } = new List<CBT_ScormRegistration>();

        public bool IsFinalGradePassing { get { return (FinalGrade ?? "").ToUpper() == "P"; } }

        public bool IsComplete { get { return CompletionDate.HasValue; } }
        public virtual PublicClassScheduleRequest PublicClassScheduleRequest { get; set; }
        public virtual ICollection<ClassScheduleEmployee_ILACertificationLink_PartialCredit> ClassScheduleEmployee_ILACertificationLink_PartialCredits { get; set; } = new List<ClassScheduleEmployee_ILACertificationLink_PartialCredit>();

        public void ApproveSelfRegistration()
        {
            AddDomainEvent(new Domain.Events.Core.OnSelfRegistrationApproved(this));
        }
        public void DeclineSelfRegistration()
        {
            this.IsDenied = true;
            this.IsWaitlisted = false;

            AddDomainEvent(new Domain.Events.Core.OnSelfRegistrationDenied(this));
        }

        public ClassSchedule_Employee()
        {
        }
        public ClassSchedule_Employee(ClassSchedule classSchedule, Employee employee) : this()
        {
            ClassSchedule = classSchedule;
            Employee = employee;
            ClassScheduleId = classSchedule.Id;
            EmployeeId = employee.Id;
        }

        public void UpdateGradeNotes(string? notes)
        {
            GradeNotes = notes;
        }

        public void CompleteClass(DateTime? completionDate, string grade, int? score, string reason = null)
        {
            if(reason != null)
            {
                GradeNotes = reason;
                AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Employee_UpdatedCompletionInfo(this));
            }
            if(completionDate.HasValue)
            {
                CompletionDate = completionDate;
                AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Employee_Completed(this));
                AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Employee_UpdatedCompletionInfo(this));
            }
            if(!string.IsNullOrEmpty(grade))
            {
                FinalGrade = grade;
                AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Employee_UpdatedCompletionInfo(this));
            }
            if(score.HasValue)
            {
                FinalScore = score;
                AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Employee_UpdatedCompletionInfo(this));
            }                   
        }
        public void EnrollStudent(System.DateTime? plannedDate,bool isCallHandler = true)
        {
            this.IsEnrolled = true;
            this.IsWaitlisted = false;
            this.IsDenied = false;
            this.IsDropped = false;
            this.IsAwaitingForApproval = false;
            this.EnrolledDate = DateTime.Now.ToUniversalTime();
            this.PlannedDate = plannedDate;
            if (isCallHandler)
            {
                AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Employee_Enrolled(this));
            }
        }

        public void UnenrollStudent()
        {
            this.IsEnrolled = false;
            this.PlannedDate = null;
            this.EnrolledDate = null;

            AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Employee_Unenrolled(this));
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Employee_Deleted(this));
        }

        public void DropCourse()
        {
            this.IsDropped = true;
            this.IsDenied = false;
            this.IsEnrolled = false;
            this.IsWaitlisted = false;
            AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Employee_Unenrolled(this));
        }

    }
}
