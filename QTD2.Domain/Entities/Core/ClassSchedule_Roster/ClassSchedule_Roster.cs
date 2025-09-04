using QTD2.Domain.Events.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule_Roster : Common.Entity
    {
        public bool IsFromMetaIla { get { return MetaIla_EmployeeId.HasValue; } }
        public bool IsFromClassSchedule { get { return ClassScheduleId.HasValue; } }

        public int? MetaIla_EmployeeId { get; set; }

        public int? ClassScheduleId { get; set; }

        public int TestId { get; set; }

        public int TestTypeId { get; set; }

        public int EmpId { get; set; }

        public bool Disclaimer { get; set; }

        public string Grade { get; set; }

        public bool IsGradePassing { get { return (Grade ?? "").ToUpper() == "P"; } }

        public bool Interrupted { get; set; }

        public bool Restarted { get; set; }

        public DateTime? CompletedDate { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? Score { get; set; }

        public bool? IsReleased { get; set; }

        public int? StatusId { get; set; }

        public int? RetakeOrder { get; set; }

        public virtual Test Test { get; set; }

        public virtual TestType TestType { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ClassSchedule ClassSchedule { get; set; }

        public virtual ICollection<ClassSchedule_Roster_Response> Responses { get; set; }

        public virtual MetaILA_Employee MetaILA_Employee { get; set; }

        public virtual ClassSchedule_Roster_Statuses Status { get; set; }
       
        public virtual ICollection<ClassSchedule_Roster_TimeRecord> TimeRecords { get; set; }
       
        public ClassSchedule_Roster()
        {
        }

        public override void Create(string username)
        {
            base.Create(username);
        }

        public ClassSchedule_Roster(int classScheduleId, int testId, int testTypeId, int empId, bool disclaimer, string grade, bool interrupted, bool restarted, DateTime? completedDate, DateTime? releaseDate, int? score, bool? isReleased, int? statusId)
        {
            ClassScheduleId = classScheduleId;
            TestId = testId;
            TestTypeId = testTypeId;
            EmpId = empId;
            Disclaimer = disclaimer;
            Grade = grade;
            Interrupted = interrupted;
            Restarted = restarted;
            CompletedDate = completedDate;
            ReleaseDate = releaseDate;
            Score = score;
            IsReleased = isReleased;
            StatusId = statusId;
        }


        public ClassSchedule_Roster(int metaIla_employeeId, int testId, int empId)
        {
            MetaIla_EmployeeId = metaIla_employeeId;
            TestTypeId = 6;
            EmpId = empId;
            TestId = testId;
        }

        public void Release(DateTime date)
        {
            ReleaseDate = date;
            IsReleased = true;
            RetakeOrder = null;

            AddDomainEvent(new Domain.Events.Core.OnTestReleased(this));
        }
        public void CompleteTest(DateTime? completedDate, int? score, string grade)
        {
            this.StatusId = 3;
            CompletedDate = completedDate;
            Grade = grade;
            Score = score;
            AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Roster_TestCompleted(this));
        }

        public void CompleteTest(DateTime? completedDate, int? score, string grade, string modifiedBy)
        {
            this.CompleteTest(completedDate, score, grade);

            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = DateTime.Now.ToUniversalTime();
        }

        public void MarkAsIncomplete(string username)
        {
            this.StatusId = 2;
            this.Modify(username);
        }

        public TimeSpan TotalTestDuration
        {
            get
            {
                if (TimeRecords == null)
                {
                    return TimeSpan.Zero;
                }

                var totalDurationTicks = TimeRecords.Sum(record => (record.EndDateTime - record.StartDateTime).Ticks);

                return TimeSpan.FromTicks(totalDurationTicks);
            }
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Roster_Deleted(this));
        }

    }
}
