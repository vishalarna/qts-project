using QTD2.Domain.Entities.Core;
using Xunit;
using TaskEntity = QTD2.Domain.Entities.Core.Task;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class TaskTests
    {
        [Theory, MemberData(nameof(TaskTests.Tasks_ProcedureGetData))]
        public void Task_LinkProcedure(TaskEntity task, Procedure procedure)
        {
            var procCount = task.Procedure_Task_Links.Count;

            task.LinkProcedure(procedure, false);

            Assert.Equal(procCount + 1, task.Procedure_Task_Links.Count);
        }

        [Theory, MemberData(nameof(TaskTests.Tasks_ProcedureGetData))]
        public void Task_UnLinkProcedure(TaskEntity task, Procedure procedure)
        {
            var procCount = task.Procedure_Task_Links.Count;

            task.LinkProcedure(procedure, false);
            task.UnlinkProcedure(procedure);

            Assert.Equal(procCount, task.Procedure_Task_Links.Count);
        }
    }
}
