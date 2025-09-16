using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.TaskReQualificationEmp
{
    public class TaskReQualificationEmpQuestionVM
    {
        public List<QuesionAnswer> QuesionAnswerList { get; set; } = new List<QuesionAnswer>();
        public string TaskDescription { get; set; }
        public int TraineeId { get; set; }
        public int TaskQualificationId { get; set; }
        public int TaskId { get; set; }
        public string? SkillNumber { get; set; }
        public string? SkillDescription { get; set; }
        public int? SkillId { get; set; }
        public string? concateNatedSkillNumber { get; set; }
        public int SkillQualificationId { get; set; }

    }
    public class QuesionAnswer
    {
        public int QuestionId { get; set; }

        public string QuestionDescription { get; set; }
        public string Answer { get; set; }

        public string Comments { get; set; }

        public bool IsCompleted { get; set; }

        public List<QuestionAnswerCommentsVM> EvaluatorsQuestionAnswerComments { get; set; } = new List<QuestionAnswerCommentsVM>();
    }

    public class QuestionAnswerCommentsVM
    {
        public string EmployeeName { get; set; }
        public string Comment { get; set; }
        public DateTime CommentDate { get; set; }
        public string Image { get; set; }

    }
}
