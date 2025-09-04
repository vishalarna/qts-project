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
        [Theory, MemberData(nameof(ILATest.GetILA_NERCAudienceTestData))]
        public void ILATest_LinkNERCAudience(ILA ila, NERCTargetAudience nta)
        {
            var ilaCount = ila.ILA_NERCAudience_Links.Count();
            ila.LinkNERCAudience(nta);
            Assert.Equal(ilaCount + 1, ila.ILA_NERCAudience_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_NERCAudienceTestData))]
        public void ILATest_UnlinkNERCAudience(ILA ila, NERCTargetAudience nta)
        {
            var ilaCount = ila.ILA_NERCAudience_Links.Count();
            ila.LinkNERCAudience(nta);
            ila.UnlinkNERCAudience(nta);
            Assert.Equal(ilaCount, ila.ILA_NERCAudience_Links.Count());
        }
    }
}
