using QTD2.Domain.Entities.Core;
using System.Linq;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class ProcedureTests
    {
        [Theory, MemberData(nameof(ProcedureTests.GetProcedure_EnablingObjectiveTestData))]
        public void ProcedureTests_LinkEnablingObjective(Procedure procedure, EnablingObjective eo)
        {
            var eoCount = procedure.Procedure_EnablingObjective_Links.Count();

            procedure.LinkEnablingObjective(eo);

            Assert.Equal(eoCount + 1, procedure.Procedure_EnablingObjective_Links.Count());
        }

        [Theory, MemberData(nameof(ProcedureTests.GetProcedure_EnablingObjectiveTestData))]
        public void ProcedureTests_UnLinkEnablingObjective(Procedure procedure, EnablingObjective eo)
        {
            var eoCount = procedure.Procedure_EnablingObjective_Links.Count();

            procedure.LinkEnablingObjective(eo);
            procedure.UnlinkEnablingObjective(eo);

            Assert.Equal(eoCount, procedure.Procedure_EnablingObjective_Links.Count());
        }
    }
}
