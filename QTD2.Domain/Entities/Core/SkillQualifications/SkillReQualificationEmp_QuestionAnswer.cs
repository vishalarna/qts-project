using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Domain.Entities.Core
{
    public class SkillReQualificationEmp_QuestionAnswer : Common.Entity
    {
        public int SkillQualificationId { get; set; }
        public int SkillQuestionId { get; set; }
        public string Comments { get; set; }
        public int EvaluatorId { get; set; }
        public DateTime CommentDate { get; set; }
        public int TraineeId { get; set; }
        public bool IsCompleted { get; set; }
        public virtual EnablingObjective_Question EnablingObjective_Question { get; set; }
        public virtual Employee Evaluator { get; set; }
        public virtual Employee Trainee { get; set; }
        public virtual SkillQualification SkillQualification { get; set; }

        public SkillReQualificationEmp_QuestionAnswer(int skillQualificationId, int skillQuestionId, string comments, int evaluatorId, DateTime commentDate, int traineeId, bool isCompleted)
        {

            SkillQualificationId = skillQualificationId;
            SkillQuestionId = skillQuestionId;
            Comments = comments;
            EvaluatorId = evaluatorId;
            CommentDate = commentDate;
            TraineeId = traineeId;
            IsCompleted = isCompleted;

        }
        public SkillReQualificationEmp_QuestionAnswer()
        {

        }
    }
}
