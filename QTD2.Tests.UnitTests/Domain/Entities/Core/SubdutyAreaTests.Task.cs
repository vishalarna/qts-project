using QTD2.Domain.Entities.Core;
using System.Linq;
using Xunit;
using TaskEntity = QTD2.Domain.Entities.Core.Task;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class SubdutyAreaTests
    {
        [Theory, MemberData(nameof(SubdutyAreaTests.GetTaskData))]
        public void SubdutyArea_AddTask(SubdutyArea subdutyArea, TaskEntity task)
        {
            var taskCount = subdutyArea.Tasks.Count();

            subdutyArea.AddTask(task);

            Assert.Equal(taskCount + 1, subdutyArea.Tasks.Count());
        }

        [Theory, MemberData(nameof(SubdutyAreaTests.GetTaskData))]
        public void SubdutyArea_RemoveTask(SubdutyArea subdutyArea, TaskEntity task)
        {
            var taskCount = subdutyArea.Tasks.Count();

            subdutyArea.AddTask(task);
            subdutyArea.RemoveTask(task);

            Assert.Equal(taskCount, subdutyArea.Tasks.Count());
        }
    }
}
