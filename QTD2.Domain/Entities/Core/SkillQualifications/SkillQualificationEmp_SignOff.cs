using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class SkillQualificationEmp_SignOff : Common.Entity
    {
        public int SkillQualificationId { get; set; }
        public bool? IsCriteriaMet { get; set; }
        public string Comments { get; set; }
        public int EvaluatorId { get; set; }
        public int? EvaluationMethodId { get; set; }
        public DateTime? SkillQualificationDate { get; set; }
        public int TraineeId { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsStarted { get; set; }
        public bool? IsLocked { get; set; }
        public DateTime? SignOffDate { get; set; }
        public bool? IsTraineeSignOff { get; set; }
        public bool? IsEvaluatorSignOff { get; set; }
        public virtual EvaluationMethod EvaluationMethod { get; set; }
        public virtual Employee Evaluator { get; set; }
        public virtual Employee Trainee { get; set; }
        public virtual SkillQualification SkillQualification { get; set; }

        public SkillQualificationEmp_SignOff()
        {

        }

        public SkillQualificationEmp_SignOff(int skillQualificationId, bool? isCriteriaMet, string comments, int evaluatorId, int? evaluationMethodId, DateTime? skillQualificationDate, int traineeId, bool? isCompleted, bool? isStarted, bool? isLocked, DateTime? signOffDate, bool? isTraineeSignOff, bool? isEvaluatorSignOff)
        {
            SkillQualificationId = skillQualificationId;
            IsCriteriaMet = isCriteriaMet;
            Comments = comments;
            EvaluatorId = evaluatorId;
            EvaluationMethodId = evaluationMethodId;
            SkillQualificationDate = skillQualificationDate;
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
