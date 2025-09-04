namespace QTD2.Infrastructure.Model.Task_Step
{
    public class Task_StepCreateOptions
    {
        public string Description { get; set; }

        public int Number { get; set; }

        public int ParentStepId { get; set; }

        public bool isSignificant { get; set; }
    }
}
