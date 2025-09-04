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
        [Theory, MemberData(nameof(RegulatoryRequirementTest.GetRegulatoryRequirement_ILATestData))]
        public void RegulatoryRequirementTest_LinkILA(RegulatoryRequirement rr, ILA ila)
        {
            var rrCount = rr.ILA_RegRequirement_Links.Count();

            rr.LinkILA(ila);

            Assert.Equal(rrCount + 1, rr.ILA_RegRequirement_Links.Count());
        }

        [Theory, MemberData(nameof(RegulatoryRequirementTest.GetRegulatoryRequirement_ILATestData))]
        public void RegulatoryRequirementTest_UninkILA(RegulatoryRequirement rr, ILA ila)
        {
            var rrCount = rr.ILA_RegRequirement_Links.Count();

            rr.LinkILA(ila);
            rr.UnlinkIla(ila);

            Assert.Equal(rrCount, rr.ILA_RegRequirement_Links.Count());
        }
    }
}
