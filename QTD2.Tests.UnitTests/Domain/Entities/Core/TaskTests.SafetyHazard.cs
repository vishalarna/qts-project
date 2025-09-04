using QTD2.Domain.Entities.Core;
using Xunit;
using TaskEntity = QTD2.Domain.Entities.Core.Task;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class TaskTests
    {
        [Theory, MemberData(nameof(TaskTests.Tasks_SafetyHazardGetData))]
        public void Task_LinkSaftyHazard(TaskEntity task, SaftyHazard SaftyHazard)
        {
            var shCount = task.SafetyHazard_Task_Links.Count;

            task.LinkSaftyHazard(SaftyHazard);

            Assert.Equal(shCount + 1, task.SafetyHazard_Task_Links.Count);
        }

        [Theory, MemberData(nameof(TaskTests.Tasks_SafetyHazardGetData))]
        public void Task_UnLinkSaftyHazard(TaskEntity task, SaftyHazard SaftyHazard)
        {
            var shCount = task.SafetyHazard_Task_Links.Count;

            task.LinkSaftyHazard(SaftyHazard);
            task.UnlinkSaftyHazard(SaftyHazard);

            Assert.Equal(shCount, task.SafetyHazard_Task_Links.Count);
        }
    }
}
