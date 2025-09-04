using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class ILATest
    {
        [Theory, MemberData(nameof(ILATest.GetILA_CustomEnablingObjectiveTestData))]
        public void ILATest_LinkCustomEnablingObjective(ILA ila, CustomEnablingObjective eo)
        {
            var ilaCount = ila.ILACustomObjective_Links.Count();
            ila.LinkCustomEnablingObjective(eo);
            Assert.Equal(ilaCount + 1, ila.ILACustomObjective_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_CustomEnablingObjectiveTestData))]
        public void ILATest_UnlinkCustomEnablingObjective(ILA ila, CustomEnablingObjective eo)
        {
            var ilaCount = ila.ILACustomObjective_Links.Count();
            ila.LinkCustomEnablingObjective(eo);
            ila.UnLinkCustomEnablingObjective(eo);
            Assert.Equal(ilaCount, ila.ILA_EnablingObjective_Links.Count());
        }
    }
}
