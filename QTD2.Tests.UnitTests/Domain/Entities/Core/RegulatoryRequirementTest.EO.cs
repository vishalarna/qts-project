using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class RegulatoryRequirementTest
    {
        [Theory, MemberData(nameof(RegulatoryRequirementTest.GetRegulatoryRequirement_EOTestdata))]
        public void RRTest_LinkEO(RegulatoryRequirement rr, EnablingObjective eo)
        {
            var rrCount = rr.RegRequirement_EO_Links.Count();
            rr.LinkEO(eo);
            Assert.Equal(rrCount + 1, rr.RegRequirement_EO_Links.Count());
        }

        [Theory, MemberData(nameof(RegulatoryRequirementTest.GetRegulatoryRequirement_EOTestdata))]
        public void RRTest_UnlinkEO(RegulatoryRequirement rr, EnablingObjective eo)
        {
            var rrCount = rr.RegRequirement_EO_Links.Count();
            rr.LinkEO(eo);
            rr.UnlinkEO(eo);
            Assert.Equal(rrCount, rr.RegRequirement_EO_Links.Count());
        }
    }
}
