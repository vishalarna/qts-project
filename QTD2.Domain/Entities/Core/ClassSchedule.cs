using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class ClassSchedule : Common.Entity
    {
        public int? RecurrenceId { get; set; }

        public bool IsRecurring { get; set; }

        public int? ProviderID { get; set; }

        public int? ILAID { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public int? InstructorId { get; set; }

        public int? LocationId { get; set; }

        public string SpecialInstructions { get; set; }

        public string WebinarLink { get; set; }

        public int? ClassSize { get; set; }

        public string Notes { get; set; }

        public bool IsStartAndEndTimeEmpty { get; set; } = false;
        public bool IsPubliclyAvailable { get; set; }

        public virtual Location Location { get; set; }

        [Obsolete("Use ILA.Provider instead")]
        public virtual Provider Provider { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual ILA ILA { get; set; }
        public virtual ClassSchedule_SelfRegistrationOptions ClassSchedule_SelfRegistrationOption { get; set; }
        public virtual ICollection<IDPSchedule> IDPSchedules { get; set; } = new List<IDPSchedule>();
        public virtual ICollection<ClassSchedule_Evaluation_Roster> ClassSchedule_Evaluation_Rosters { get; set; } = new List<ClassSchedule_Evaluation_Roster>();

        public virtual ICollection<ClassScheduleHistory> ClassScheduleHistories { get; set; } = new List<ClassScheduleHistory>();

        public virtual ICollection<ClassSchedule_Employee> ClassSchedule_Employee { get; set; } = new List<ClassSchedule_Employee>();

        public virtual ICollection<ClassSchedule_StudentEvaluations_Link> ClassSchedule_StudentEvaluations_Links { get; set; } = new List<ClassSchedule_StudentEvaluations_Link>();

        public virtual ClassSchedule_Recurrence ClassSchedule_Recurrence { get; set; }

        public virtual List<ClassSchedule> Recurrences { get; set; }

        public virtual List<StudentEvaluationWithoutEmp> StudentEvaluationWithoutEmps { get; set; } = new List<StudentEvaluationWithoutEmp>();

        public virtual ICollection<ClassSchedule_Roster> ClassSchedule_Rosters { get; set; } = new List<ClassSchedule_Roster>();
        public virtual ClassSchedule_TestReleaseEMPSetting ClassSchedule_TestReleaseEMPSettings { get; set; }
        //public virtual ICollection<EmpTest> EmpTests { get; set; } = new List<EmpTest>();
        public virtual ClassSchedule_TQEMPSetting ClassSchedule_TQEMPSettings { get; set; } 
        public virtual ICollection<ClassSchedule_Evaluator_Link> ClassSchedule_Evaluator_Links { get; set; } = new List<ClassSchedule_Evaluator_Link>();
        public virtual ICollection<TaskQualification> TaskQualifications { get; set; } = new List<TaskQualification>();
        public virtual ICollection<PublicClassScheduleRequest> PublicClassScheduleRequests { get; set; } = new List<PublicClassScheduleRequest>();
        public virtual ICollection<SkillQualification> SkillQualifications { get; set; } = new List<SkillQualification>();
        public ClassSchedule()
        {

        }
        public ClassSchedule(int? recurrenceId, int providerID, int iLAID, DateTime startDate, DateTime endDate, int? instructorId, int? locationId, string specialInstructions, string webinarLink, string notes, int classSize, bool isStartEndTimeEmpty, bool isPubliclyAvailable)
        {
            AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Create(this, iLAID));
            RecurrenceId = recurrenceId;
            ProviderID = providerID;
            ILAID = iLAID;
            StartDateTime = startDate;
            EndDateTime = endDate;
            InstructorId = instructorId;
            LocationId = locationId;
            SpecialInstructions = specialInstructions;
            WebinarLink = webinarLink;
            Notes = notes;
            ClassSize = classSize;
            IsStartAndEndTimeEmpty = isStartEndTimeEmpty;
            IsPubliclyAvailable = isPubliclyAvailable;
        }

        public ClassSchedule(int providerID, int iLAID, DateTime startDate, DateTime endDate, int? instructorId, int? locationId, bool persist)
        {
            if (persist) { 
                AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Create(this, iLAID));
            }

            ProviderID = providerID;
            ILAID = iLAID;
            StartDateTime = startDate;
            EndDateTime = endDate;
            InstructorId = instructorId;
            LocationId = locationId;
            IsRecurring = false;
            IsStartAndEndTimeEmpty = false;
        }

        public override void Delete()
        { 
            AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Delete(this));
            base.Delete();
        }

        public void Update()
        {
            AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Update(this));
        }

        public ClassSchedule_Employee LinkEmployee(Employee employee,bool enroll =  false)
        {
            ClassSchedule_Employee classSchedule_employee_link = ClassSchedule_Employee.FirstOrDefault(x => x.EmployeeId == employee.Id && x.ClassScheduleId == this.Id);
            if (classSchedule_employee_link != null)
            {
                return classSchedule_employee_link;
            }

            classSchedule_employee_link = new ClassSchedule_Employee(this, employee);
            classSchedule_employee_link.CBTStatusId = 1;
            classSchedule_employee_link.TestStatusId = 1;
            classSchedule_employee_link.PreTestStatusId = 1;
            classSchedule_employee_link.RetakeStatusId = 1;

            AddEntityToNavigationProperty<ClassSchedule_Employee>(classSchedule_employee_link);
            return classSchedule_employee_link;
        }
        public void UnlinkEmployee(Employee employee)
        {
            ClassSchedule_Employee classSchedule_employee_link = ClassSchedule_Employee.FirstOrDefault(x => x.EmployeeId == employee.Id);
            if (classSchedule_employee_link != null)
            {
                RemoveEntityFromNavigationProperty<ClassSchedule_Employee>(classSchedule_employee_link);
            }
        }

        public ClassSchedule_Employee RegisterEmployee(Employee employee, bool requiredAdminApproval)
        {
            ClassSchedule_Employee classSchedule_employee_link = ClassSchedule_Employee.FirstOrDefault(x => x.EmployeeId == employee.Id && x.ClassScheduleId == this.Id);
            if (classSchedule_employee_link != null)
            {
                return classSchedule_employee_link;
            }

            classSchedule_employee_link = new ClassSchedule_Employee(this, employee);
            classSchedule_employee_link.CBTStatusId = 1;
            classSchedule_employee_link.TestStatusId = 1;
            classSchedule_employee_link.PreTestStatusId = 1;
            classSchedule_employee_link.RetakeStatusId = 1;
            classSchedule_employee_link.IsWaitlisted = false;
            classSchedule_employee_link.IsDropped = false;
            classSchedule_employee_link.IsDenied = false;
            if (requiredAdminApproval == false)
            {
                classSchedule_employee_link.EnrollStudent(null);
            }
            else
            {
                classSchedule_employee_link.IsAwaitingForApproval = true;
            }
            AddEntityToNavigationProperty<ClassSchedule_Employee>(classSchedule_employee_link);
            return classSchedule_employee_link;
        }
        public ClassSchedule_Employee DropEmployeeCourse(Employee employee)
        {
            ClassSchedule_Employee classSchedule_employee_link = ClassSchedule_Employee.FirstOrDefault(x => x.EmployeeId == employee.Id && x.ClassScheduleId == this.Id);
            if (classSchedule_employee_link != null)
            {
                return classSchedule_employee_link;
            }

            classSchedule_employee_link = new ClassSchedule_Employee(this, employee);
            classSchedule_employee_link.CBTStatusId = 1;
            classSchedule_employee_link.TestStatusId = 1;
            classSchedule_employee_link.PreTestStatusId = 1;
            classSchedule_employee_link.RetakeStatusId = 1;
            classSchedule_employee_link.IsDropped = true;
            classSchedule_employee_link.IsDenied = false;
            classSchedule_employee_link.IsEnrolled = false;
            classSchedule_employee_link.IsWaitlisted = false;

            AddEntityToNavigationProperty<ClassSchedule_Employee>(classSchedule_employee_link);
            return classSchedule_employee_link;
        }

        public ClassSchedule_Employee JoinWaitList(Employee employee)
        {
            ClassSchedule_Employee classSchedule_employee_link = ClassSchedule_Employee.FirstOrDefault(x => x.EmployeeId == employee.Id && x.ClassScheduleId == this.Id);
            if (classSchedule_employee_link != null)
            {
                return classSchedule_employee_link;
            }

            classSchedule_employee_link = new ClassSchedule_Employee(this, employee);
            classSchedule_employee_link.CBTStatusId = 1;
            classSchedule_employee_link.TestStatusId = 1;
            classSchedule_employee_link.PreTestStatusId = 1;
            classSchedule_employee_link.RetakeStatusId = 1;
            classSchedule_employee_link.IsWaitlisted = true;
            AddEntityToNavigationProperty<ClassSchedule_Employee>(classSchedule_employee_link);
            return classSchedule_employee_link;
        }

        public ClassSchedule_StudentEvaluations_Link LinkStudentEvaluation(StudentEvaluation studentEvaluation)
        {
            ClassSchedule_StudentEvaluations_Link classSchedule_StudentEvaluations_Link = ClassSchedule_StudentEvaluations_Links.FirstOrDefault(x => x.StudentEvaluationId == studentEvaluation.Id);
            if (classSchedule_StudentEvaluations_Link != null)
            {
                return classSchedule_StudentEvaluations_Link;
            }

            classSchedule_StudentEvaluations_Link = new ClassSchedule_StudentEvaluations_Link(studentEvaluation, this);

            AddEntityToNavigationProperty<ClassSchedule_StudentEvaluations_Link>(classSchedule_StudentEvaluations_Link);
            return classSchedule_StudentEvaluations_Link;
        }

        public ClassSchedule_Evaluator_Link LinkEvaluator(Employee evaluator)
        {
            ClassSchedule_Evaluator_Link classSchedule_Evaluator_Link = ClassSchedule_Evaluator_Links.FirstOrDefault(x => x.EvaluatorId == evaluator.Id && this.Id == x.ClassScheduleId);
            if (classSchedule_Evaluator_Link != null)
            {
                return classSchedule_Evaluator_Link;
            }

            classSchedule_Evaluator_Link = new ClassSchedule_Evaluator_Link(this.Id, evaluator.Id);
            AddEntityToNavigationProperty<ClassSchedule_Evaluator_Link>(classSchedule_Evaluator_Link);
            AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Evaluator_Link(this,evaluator));
            return classSchedule_Evaluator_Link;
        }

        public void UnlinkEvaluator(Employee evaluator)
        {
            ClassSchedule_Evaluator_Link classSchedule_Evaluator_Link = ClassSchedule_Evaluator_Links.FirstOrDefault(x => x.EvaluatorId == evaluator.Id && this.Id == x.ClassScheduleId);
            if (classSchedule_Evaluator_Link != null)
            {
                RemoveEntityFromNavigationProperty<ClassSchedule_Evaluator_Link>(classSchedule_Evaluator_Link);
                AddDomainEvent(new Domain.Events.Core.OnClassSchedule_Evaluator_Unlink(this,evaluator));
            }
        }
    }
}

