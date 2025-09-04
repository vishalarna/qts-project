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
        [Theory, MemberData(nameof(ILATest.GetILA_SafetyHazardTestData))]
        public void ILATest_LinkSafetyHazard(ILA ila, SaftyHazard sh)
        {
            var ilaCount = ila.ILA_SafetyHazard_Links.Count();
            ila.LinkSafetyHazards(sh);
            Assert.Equal(ilaCount + 1, ila.ILA_SafetyHazard_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_SafetyHazardTestData))]
        public void ILATest_UnlinkSafetyHazard(ILA ila, SaftyHazard sh)
        {
            var ilaCount = ila.ILA_SafetyHazard_Links.Count();
            ila.LinkSafetyHazards(sh);
            ila.UnlinkSafetyHazards(sh);
            Assert.Equal(ilaCount, ila.ILA_SafetyHazard_Links.Count());
        }
    }
}
