using QTD2.Domain.Entities.Core;
using Xunit;
using TaskEntity = QTD2.Domain.Entities.Core.Task;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class TaskTests
    {
        [Theory, MemberData(nameof(TaskTests.Tasks_QuestionsGetData))]
        public void Task_AddQuestion(TaskEntity task, Task_Question question)
        {
            var qCount = task.Task_Questions.Count;

            task.AddQuestion(question);

            Assert.Equal(qCount + 1, task.Task_Questions.Count);
        }

        [Theory, MemberData(nameof(TaskTests.Tasks_QuestionsGetData))]
        public void Task_RemoveQuestion(TaskEntity task, Task_Question question)
        {
            var qCount = task.Task_Questions.Count;

            task.AddQuestion(question);
            task.RemoveQuestion(question);

            Assert.Equal(qCount, task.Task_Questions.Count);
        }
    }
}
