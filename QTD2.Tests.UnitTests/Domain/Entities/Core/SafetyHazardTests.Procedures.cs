using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class SafetyHazardTests
    {
        [Theory, MemberData(nameof(SafetyHazardTests.GetSafetyHazard_Procedure))]
        public void SafetyHazard_AddProcedure(Procedure pr, SaftyHazard sh)
        {
            var prCount = sh.Procedure_SaftyHazard_Links.Count();
            sh.LinkProcedure(pr);
            Assert.Equal(prCount + 1, sh.Procedure_SaftyHazard_Links.Count());
        }
        [Theory, MemberData(nameof(SafetyHazardTests.GetSafetyHazard_Procedure))]
        public void SafetyHazard_RemoveProcedure(Procedure pr, SaftyHazard sh)
        {
            var prCount = sh.Procedure_SaftyHazard_Links.Count();
            sh.LinkProcedure(pr);
            sh.UnLinkProcedure(pr);
            Assert.Equal(prCount, sh.Procedure_SaftyHazard_Links.Count());
        }
    }
}
