using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class MetaILATest
    {
        [Theory, MemberData(nameof(MetaILATest.GetMeta_ILAMembers_LinkTestData))]
        public void MetaILATest_LinkILAMemebers(MetaILA meta_ila, ILA ila, MetaILAConfigurationPublishOption metaILAConfigurationPublishOption, int sequenceNumber)
        {
            var ilaCount = meta_ila.Meta_ILAMembers_Links.Count();
            meta_ila.LinkILAMemebers(ila, metaILAConfigurationPublishOption, sequenceNumber, DateTime.Now);
            Assert.Equal(ilaCount + 1, meta_ila.Meta_ILAMembers_Links.Count());
        }

        [Theory, MemberData(nameof(MetaILATest.GetMeta_ILAMembers_LinkTestData))]
        public void MetaILATest_UnLinkILAMemebers(MetaILA meta_ila, ILA ila, MetaILAConfigurationPublishOption metaILAConfigurationPublishOption, int sequenceNumber)
        {
            var ilaCount = meta_ila.Meta_ILAMembers_Links.Count();
            meta_ila.LinkILAMemebers(ila, metaILAConfigurationPublishOption, sequenceNumber, DateTime.Now);
            meta_ila.UnlinkILAMemeber(ila);
            Assert.Equal(ilaCount, meta_ila.Meta_ILAMembers_Links.Count());
        }
    }
}
