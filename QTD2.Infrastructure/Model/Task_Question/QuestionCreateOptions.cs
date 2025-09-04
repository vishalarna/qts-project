namespace QTD2.Infrastructure.Model.Task_Question
{
    public class QuestionCreateOptions
    {
        public string Question { get; set; }

        public string Answer { get; set; }

        public bool IsSignificant { get; set; }

        public int QuestionNumber { get; set; }
    }
}
