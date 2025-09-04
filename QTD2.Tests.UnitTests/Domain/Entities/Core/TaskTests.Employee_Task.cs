using QTD2.Domain.Entities.Core;
using Xunit;
using TaskEntity = QTD2.Domain.Entities.Core.Task;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class TaskTests
    {
        [Theory, MemberData(nameof(TaskTests.Tasks_EmployeeGetData))]
        public void Task_CreateEmployeeTask(TaskEntity task, Employee employee)
        {
            var empCount = task.Employee_Tasks.Count;

            task.CreateEmployeeTask(employee);

            Assert.Equal(empCount + 1, task.Employee_Tasks.Count);
        }
    }
}
