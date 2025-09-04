namespace QTD2.Domain.Entities.Core
{
    public class Version_Task_Question : Common.Entity
    {
        public int TaskQuestionId { get; set; }

        public int VersionTaskId { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public int QuestionNumber { get; set; }

        public virtual Version_Task Version_Task { get; set; }

        public virtual Task_Question Task_Question { get; set; }

        public Version_Task_Question()
        {
        }

        public Version_Task_Question(Task_Question question)
        {
            TaskQuestionId = question.Id;
            VersionTaskId = question.TaskId;
            Question = question.Question;
            Answer = question.Answer;
            QuestionNumber = question.QuestionNumber;
        }

        public Version_Task_Question(Version_Task task, Task_Question question)
        {
            TaskQuestionId = question.Id;
            VersionTaskId = task.Id;
            Question = question.Question;
            Answer = question.Answer;
            QuestionNumber = question.QuestionNumber;
        }
    }
}
