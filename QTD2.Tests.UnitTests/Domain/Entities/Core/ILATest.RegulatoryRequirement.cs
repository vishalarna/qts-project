using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class ILATest
    {
        [Theory, MemberData(nameof(ILATest.GetILA_RegulatoryRequirementTestData))]
        public void ILATest_LinkRegulatoryRequirement(ILA ila, RegulatoryRequirement regulatoryRequirement)
        {
            var rrCount = ila.ILA_RegRequirement_Links.Count();

            ila.LinkRegRequirement(regulatoryRequirement);

            Assert.Equal(rrCount + 1, ila.ILA_RegRequirement_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_RegulatoryRequirementTestData))]
        public void ILATest_UnlinkRegulatoryRequirement(ILA ila, RegulatoryRequirement regulatoryRequirement)
        {
            var rrCount = ila.ILA_RegRequirement_Links.Count();

            ila.LinkRegRequirement(regulatoryRequirement);
            ila.UnlinkRegRequirement(regulatoryRequirement);

            Assert.Equal(rrCount, ila.ILA_RegRequirement_Links.Count());
        }
    }
}
