using QTD2.Domain.Entities.Core;
using System.Linq;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class EnablingObjectiveTests
    {
        [Theory, MemberData(nameof(EnablingObjectiveTests.GetEnablingObjective_ProcedureTestData))]
        public void EnablingObjectiveTests_LinkProcedure(EnablingObjective eo, Procedure procedure)
        {
            var procCount = eo.Procedure_EnablingObjective_Links.Count();

            eo.LinkProcedure(procedure, eo.Id);

            Assert.Equal(procCount+1, eo.Procedure_EnablingObjective_Links.Count());
        }

        [Theory, MemberData(nameof(EnablingObjectiveTests.GetEnablingObjective_ProcedureTestData))]
        public void EnablingObjectiveTests_UnLinkProcedure(EnablingObjective eo, Procedure procedure)
        {
            var procCount = eo.Procedure_EnablingObjective_Links.Count();

            eo.LinkProcedure(procedure, eo.Id);
            eo.UnlinkProcedure(procedure);

            Assert.Equal(procCount, eo.Procedure_EnablingObjective_Links.Count());
        }
    }
}
