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
        [Theory, MemberData(nameof(ILATest.GetILA_NercStandardTestData))]
        public void ILATest_LinkNercStandard(ILA ila,NercStandard nps, NercStandardMember nsm, float creditHoursByStd)
        {
            var ilaCount = ila.ILA_NercStandard_Links.Count();
            ila.LinkNercStandard(nps, nsm, creditHoursByStd);
            Assert.Equal(ilaCount + 1, ila.ILA_NercStandard_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_NercStandardTestData))]
        public void ILATest_UnlinkNercStandard(ILA ila, NercStandard nps, NercStandardMember nsm, float creditHoursByStd)
        {
            var ilaCount = ila.ILA_NercStandard_Links.Count();
            ila.LinkNercStandard(nps, nsm, creditHoursByStd);
            ila.UnlinkNercStandard(nps, nsm);
            Assert.Equal(ilaCount, ila.ILA_NercStandard_Links.Count());
        }
    }
}
