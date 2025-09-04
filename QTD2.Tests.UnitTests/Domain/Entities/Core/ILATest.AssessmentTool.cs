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
        [Theory, MemberData(nameof(ILATest.GetILA_AssessmentToolTestData))]
        public void ILATest_LinkAssessmentTool(ILA ila, AssessmentTool at)
        {
            var ilaCount = ila.ILA_AssessmentTool_Links.Count();
            ila.LinkAssessmentTool(at);
            Assert.Equal(ilaCount + 1, ila.ILA_AssessmentTool_Links.Count());
        }

        [Theory, MemberData(nameof(ILATest.GetILA_AssessmentToolTestData))]
        public void ILATest_UnlinkAssessmentTool(ILA ila, AssessmentTool at)
        {
            var ilaCount = ila.ILA_AssessmentTool_Links.Count();
            ila.LinkAssessmentTool(at);
            ila.UnlinkAssessmentTool(at);
            Assert.Equal(ilaCount, ila.ILA_AssessmentTool_Links.Count());
        }
    }
}
