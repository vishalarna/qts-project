using QTD2.Domain.Entities.Core;
using System.Linq;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class ProcedureTests
    {
        [Theory, MemberData(nameof(ProcedureTests.GetProcedure_SafetyHazardTestData))]
        public void ProcedureTests_LinkSafetyHazard(Procedure procedure, SaftyHazard sh)
        {
            var shCount = procedure.Procedure_SaftyHazard_Links.Count();

            procedure.LinkSaftyHazard(sh);

            Assert.Equal(shCount + 1, procedure.Procedure_SaftyHazard_Links.Count());
        }

        [Theory, MemberData(nameof(ProcedureTests.GetProcedure_SafetyHazardTestData))]
        public void ProcedureTests_UnLinkSafetyHazard(Procedure procedure, SaftyHazard sh)
        {
            var shCount = procedure.Procedure_SaftyHazard_Links.Count();

            procedure.LinkSaftyHazard(sh);
            procedure.UnlinkSaftyHazard(sh);

            Assert.Equal(shCount, procedure.Procedure_SaftyHazard_Links.Count());
        }
    }
}
