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
        [Theory, MemberData(nameof(ILATest.GetILA_EnablingObjectiveTestData))]
        public void ILATest_LinkEnablingObjective(ILA ila, EnablingObjective eo)
        {
            var ilaCount = ila.ILA_EnablingObjective_Links.Count();
            ila.LinkEnablingObjective(eo);
            Assert.Equal(ilaCount + 1, ila.ILA_EnablingObjective_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_EnablingObjectiveTestData))]
        public void ILATest_UnlinkEnablingObjective(ILA ila, EnablingObjective eo)
        {
            var ilaCount = ila.ILA_EnablingObjective_Links.Count();
            ila.LinkEnablingObjective(eo);
            ila.UnlinkEnablingObjectives(eo);
            Assert.Equal(ilaCount, ila.ILA_EnablingObjective_Links.Count());
        }
    }
}
