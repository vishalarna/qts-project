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
        [Theory, MemberData(nameof(ILATest.GetILA_PositionTestData))]
        public void ILATest_LinkPosition(ILA ila, Position pos)
        {
            var ilaCount = ila.ILA_Position_Links.Count();
            ila.LinkPosition(pos);
            Assert.Equal(ilaCount + 1, ila.ILA_Position_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_PositionTestData))]
        public void ILATest_UnlinkPosition(ILA ila, Position pos)
        {
            var ilaCount = ila.ILA_Position_Links.Count();
            ila.LinkPosition(pos);
            ila.UnlinkPosition(pos);
            Assert.Equal(ilaCount, ila.ILA_Position_Links.Count());
        }
    }
}
