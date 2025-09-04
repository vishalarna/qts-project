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
        [Theory, MemberData(nameof(ILATest.GetILA_SegmentTestData))]
        public void ILATest_LinkSegment(ILA ila, Segment segment)
        {
            var segCount = ila.ILA_Segment_Links.Count();

            ila.LinkSegment(segment,1);

            Assert.Equal(segCount + 1, ila.ILA_Segment_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_SegmentTestData))]
        public void ILATest_UnlinkSegment(ILA ila, Segment segment)
        {
            var segCount = ila.ILA_Segment_Links.Count();

            ila.LinkSegment(segment,1);
            ila.UnlinkSegment(segment);

            Assert.Equal(segCount, ila.ILA_Segment_Links.Count());
        }
    }
}
