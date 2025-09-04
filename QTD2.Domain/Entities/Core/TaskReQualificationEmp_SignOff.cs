using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class TaskReQualificationEmp_SignOff : Common.Entity
    {
        public int TaskQualificationId { get; set; }
        public bool? IsCriteriaMet { get; set; }
        public string Comments { get; set; }

        public int EvaluatorId { get; set; }
        public int? EvaluationMethodId { get; set; }
        public DateTime? TaskQualificationDate { get; set; }
        public int TraineeId { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsStarted{ get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? SignOffDate { get; set; }
        public bool? IsTraineeSignOff { get; set; } 
        public bool? IsEvaluatorSignOff { get; set; } 
        public virtual EvaluationMethod EvaluationMethod { get; set; }
        public virtual Employee Evaluator { get; set; }
        public virtual Employee Trainee { get; set; }

        public virtual TaskQualification TaskQualification { get; set; }


      
        public TaskReQualificationEmp_SignOff()
        {

        }

        public TaskReQualificationEmp_SignOff(int taskQualificationId, bool? isCriteriaMet, string comments,int evaluatorId, int? evaluationMethodId, DateTime? taskQualificationDate, int traineeId, bool? isCompleted, bool? isStarted, bool? isLocked, DateTime? signOffDate , bool? isTraineeSignOff, bool? isEvaluatorSignOff)
        {
            TaskQualificationId = taskQualificationId;
            IsCriteriaMet = isCriteriaMet;
            Comments = comments;
            EvaluatorId = evaluatorId;
            EvaluationMethodId = evaluationMethodId;
            TaskQualificationDate = taskQualificationDate;
            TraineeId = traineeId;
            IsCompleted = isCompleted;
            IsStarted = isStarted;
            IsLocked = isLocked;
            SignOffDate = signOffDate;
            IsTraineeSignOff = isTraineeSignOff;
            IsEvaluatorSignOff = isEvaluatorSignOff;

        }
    }
}
