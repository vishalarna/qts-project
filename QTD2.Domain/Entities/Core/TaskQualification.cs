using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TaskQualification : Common.Entity
    {
        public int TaskId { get; set; }

        public int EmpId { get; set; }

        public int? EvaluationId { get; set; } 

        //public int? EvaluatorId { get; set; }

        public DateTime? TaskQualificationDate { get; set; }

        [Obsolete("Use TaskQualification_Evaluator_Links instead")]
        public string TaskQualificationEvaluator { get; set; }

        public DateTime? DueDate { get; set; } 
        public DateTime? RecallDate { get; set; } 

        public bool CriteriaMet { get; set; }  

        public string Comments { get; set; }  

        public bool IsReleasedToEMP { get; set; }  
        public bool IsRecalled { get; set; }  

        public int? TQStatusId { get; set; }

        public int? ClassScheduleId { get; set; }

        public int? Sequence { get; set; }

        public virtual Task Task { get; set; }  

        public virtual Employee Employee { get; set; }
         
        public virtual EvaluationMethod EvaluationMethod { get; set; }   

        public virtual Employee Evaluator { get; set; }


        public virtual ICollection<TaskQualification_Evaluator_Link> TaskQualification_Evaluator_Links { get; set; } = new List<TaskQualification_Evaluator_Link>();

        public virtual TaskQualificationStatus TaskQualificationStatus { get; set; }

        public virtual TQEmpSetting TQEmpSetting { get; set; }
        public virtual ClassSchedule? ClassSchedule { get; set; }

        public virtual ICollection<TaskReQualificationEmp_Suggestion> TaskReQualificationEmp_Suggestions { get; set; } = new List<TaskReQualificationEmp_Suggestion>();

        public virtual ICollection<TaskReQualificationEmp_Steps> TaskReQualificationEmp_Steps{ get; set; } = new List<TaskReQualificationEmp_Steps>();
        public virtual ICollection<TaskReQualificationEmp_QuestionAnswer> TaskReQualificationEmp_QuestionAnswers { get; set; } = new List<TaskReQualificationEmp_QuestionAnswer>();

        public virtual ICollection<TaskReQualificationEmp_SignOff> TaskReQualificationEmp_SignOff { get; set; } = new List<TaskReQualificationEmp_SignOff>();

        public bool IsComplete
        {
            get
            {
                return TQStatusId == 2 || TQStatusId == 4 || TQStatusId == 8;
            }
        }

        public bool IsPending
        {
            get
            {
                return TQStatusId == 3;
            }
        }


        public TaskQualification()
        {
        }

        public TaskQualification(int taskId, int empId, bool criteriaMet, bool isReleasedToEMP,int? classScheduleId, int? tqStatusId)
        {
            TaskId = taskId;
            EmpId = empId;
            CriteriaMet = criteriaMet;
            IsReleasedToEMP = isReleasedToEMP;
            ClassScheduleId = classScheduleId;
            TQStatusId = tqStatusId;
        }

        public TaskQualification_Evaluator_Link LinkEvaluator(Employee eval)
        {
            TaskQualification_Evaluator_Link tq_eval_link = TaskQualification_Evaluator_Links.Where(r => !r.Deleted && r.Active).FirstOrDefault(x => x.TaskQualificationId == this.Id && x.EvaluatorId == eval.Id);
            if(tq_eval_link != null)
            {
                return tq_eval_link;
            }
            var evalNumber = TaskQualification_Evaluator_Links.Count(x => x.TaskQualificationId == this.Id);
            tq_eval_link = new TaskQualification_Evaluator_Link(eval.Id, this.Id,evalNumber+1);
            AddEntityToNavigationProperty<TaskQualification_Evaluator_Link>(tq_eval_link);

            if (this.IsReleasedToEMP)
                AddDomainEvent(new Events.Core.OnTaskQualification_Evalutor_LinkCreated(tq_eval_link));

            return tq_eval_link;
        }

        public void UnlinkEvaluator(Employee eval)
        {
            TaskQualification_Evaluator_Link tq_eval_link = TaskQualification_Evaluator_Links.FirstOrDefault(x => x.TaskQualificationId == this.Id && x.EvaluatorId == eval.Id);
            if (tq_eval_link != null)
            {
                RemoveEntityFromNavigationProperty<TaskQualification_Evaluator_Link>(tq_eval_link);
            }
        }

        public void ClearEvaluators()
        {
            if (TaskQualification_Evaluator_Links == null) return;

            foreach(var eval in TaskQualification_Evaluator_Links)
            {
                eval.Delete();
            }
        }

        public void Release()
        {
            this.IsReleasedToEMP = true;
            AddDomainEvent(new Domain.Events.Core.OnTaskQualificationReleased(this));

            foreach(var taskQualEvaluator in this.TaskQualification_Evaluator_Links)
            {
                AddDomainEvent(new Domain.Events.Core.OnTaskQualification_Evalutor_LinkCreated(taskQualEvaluator));
            }
        }

        public void Recall()
        {
            this.IsRecalled = true;
            this.RecallDate = DateTime.UtcNow;
        }

        public void Completed()
        {
            AddDomainEvent(new Domain.Events.Core.OnTaskQualificationCompleted(this));
        }

        public override void Delete()
        {
            base.Delete();
            AddDomainEvent(new Domain.Events.Core.OnTaskQualification_Deleted(this));
        }
    }
}
