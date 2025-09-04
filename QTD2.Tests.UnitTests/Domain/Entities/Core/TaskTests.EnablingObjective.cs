using QTD2.Domain.Entities.Core;
using Xunit;
using TaskEntity = QTD2.Domain.Entities.Core.Task;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class TaskTests
    {
        [Theory, MemberData(nameof(TaskTests.Tasks_EnablingObjectiveGetData))]
        public void Task_LinkEnablingObjective(TaskEntity task, EnablingObjective enablingObjective)
        {
            var eoCount = task.Task_EnablingObjective_Links.Count;

            task.LinkEnablingObjectives(enablingObjective, false);

            Assert.Equal(eoCount + 1, task.Task_EnablingObjective_Links.Count);
        }

        [Theory, MemberData(nameof(TaskTests.Tasks_EnablingObjectiveGetData))]
        public void Task_UnLinkEnablingObjective(TaskEntity task, EnablingObjective enablingObjective)
        {
            var eoCount = task.Task_EnablingObjective_Links.Count;

            task.LinkEnablingObjectives(enablingObjective, false);
            task.UnlinkEnablingObjective(enablingObjective);

            Assert.Equal(eoCount, task.Task_EnablingObjective_Links.Count);
        }
    }
}
