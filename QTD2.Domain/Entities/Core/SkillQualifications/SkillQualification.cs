using QTD2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class SkillQualification : Entity
    {
        public int EnablingObjectiveId { get; set; }
        public int EmployeeId { get; set; }
        public int? EvaluationMethodId { get; set; }
        public int? SkillQualificationStatusId { get; set; }
        public int? ClassScheduleId { get; set; }
        public DateTime? SkillQualificationDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? RecallDate { get; set; }
        public bool CriteriaMet { get; set; }
        public string Comments { get; set; }
        public bool IsReleasedToEMP { get; set; }
        public bool IsRecalled { get; set; }
        public int? Sequence { get; set; }
        public virtual EnablingObjective EnablingObjective { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual EvaluationMethod EvaluationMethod { get; set; }
        public virtual ClassSchedule ClassSchedule { get; set; }
        public virtual SkillQualificationStatus SkillQualificationStatus { get; set; }
        public virtual ICollection<SkillQualification_Evaluator_Link> SkillQualification_Evaluator_Links { get; set; } = new List<SkillQualification_Evaluator_Link>();
        public virtual SkillQualificationEmpSetting SkillQualificationEmpSetting { get; set; }
        public virtual ICollection<SkillQualificationEmp_SignOff> SkillQualificationEmp_SignOff { get; set; } = new List<SkillQualificationEmp_SignOff>();
        public virtual ICollection<SkillReQualificationEmp_QuestionAnswer> SkillReQualificationEmp_QuestionAnswer { get; set; } = new List<SkillReQualificationEmp_QuestionAnswer>();
        public virtual ICollection<SkillReQualificationEmp_Suggestion> SkillReQualificationEmp_Suggestion { get; set; } = new List<SkillReQualificationEmp_Suggestion>();
        public virtual ICollection<SkillReQualificationEmp_Step> SkillReQualificationEmp_Step { get; set; } = new List<SkillReQualificationEmp_Step>();
        public SkillQualification() { }

        public SkillQualification(int enablingObjectiveId, int employeeId, bool criteriaMet, bool isReleasedToEMP, int? classScheduleId, int? skillQualificationStatusId)
        {
            EnablingObjectiveId = enablingObjectiveId;
            EmployeeId = employeeId;
            CriteriaMet = criteriaMet;
            IsReleasedToEMP = isReleasedToEMP;
            ClassScheduleId = classScheduleId;
            SkillQualificationStatusId = skillQualificationStatusId;
        }

        public bool IsComplete
        {
            get
            {
                return SkillQualificationStatusId == 2 || SkillQualificationStatusId == 4 || SkillQualificationStatusId == 8;
            }
        }

        public bool IsPending
        {
            get
            {
                return SkillQualificationStatusId == 3;
            }
        }

        public void Release()
        {
            this.IsReleasedToEMP = true;
        }

        public void Completed()
        {
            AddDomainEvent(new Domain.Events.Core.OnSkillQualificationCompleted(this));
        }
    }
}
