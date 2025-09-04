using QTD2.Domain.Entities.Core;
using System.Linq;
using Xunit;
using TaskEntity = QTD2.Domain.Entities.Core.Task;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class TaskTests
    {
        [Theory, MemberData(nameof(TaskTests.Tasks_StepGetData))]
        public void Task_AddStep(TaskEntity task, Task_Step task_Step)
        {
            var task_StepCount = task.Task_Steps.Count();

            task.AddStep(task_Step);

            Assert.Equal(task_StepCount + 1, task.Task_Steps.Count());
        }

        [Theory, MemberData(nameof(TaskTests.Tasks_StepGetData))]
        public void Task_RemoveStep(TaskEntity task, Task_Step task_Step)
        {
            var task_StepCount = task.Task_Steps.Count();

            task.AddStep(task_Step);
            task.RemoveStep(task_Step);

            Assert.Equal(task_StepCount, task.Task_Steps.Count());
        }
    }
}
