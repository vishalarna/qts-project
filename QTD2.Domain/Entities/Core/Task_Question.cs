using System.Collections.Generic;

namespace QTD2.Domain.Entities.Core
{
    public class Task_Question : Common.Entity
    {
        public int TaskId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public int QuestionNumber { get; set; }

        public virtual Task Task { get; set; }

        public virtual ICollection<Version_Task_Question> Version_Task_Questions { get; set; } = new List<Version_Task_Question>();

        public virtual ICollection<TaskReQualificationEmp_QuestionAnswer> TaskReQualificationEmp_QuestionAnswers { get; set; } = new List<TaskReQualificationEmp_QuestionAnswer>();
        public Task_Question()
        {
        }

        public Task_Question(int taskId, string question, string answer, int questionNumber)
        {
            TaskId = taskId;
            Question = question;
            Answer = answer;
            QuestionNumber = questionNumber;
        }

        public Version_Task_Question CreateSnapshot()
        {
            return new Version_Task_Question(this);
        }
    }
}
