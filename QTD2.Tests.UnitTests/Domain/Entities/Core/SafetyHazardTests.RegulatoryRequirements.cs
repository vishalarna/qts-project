using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTD2.Domain.Entities.Core;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class SafetyHazardTests
    {
        [Theory, MemberData(nameof(SafetyHazardTests.GetSafetyHazard_RegulatoryRequirement))]
        public void SafetyHazard_RR_AddRegRequirement(RegulatoryRequirement rr, SaftyHazard sh)
        {
            var rrCount = sh.SaftyHazard_RR_Links.Count();
            sh.LinkRegRequirement(rr);
            Assert.Equal(rrCount + 1, sh.SaftyHazard_RR_Links.Count());
        }

        [Theory, MemberData(nameof(SafetyHazardTests.GetSafetyHazard_RegulatoryRequirement))]
        public void SafetyHazard_RR_RemoveRegRequirement(RegulatoryRequirement rr, SaftyHazard sh)
        {
            var rrCount = sh.SaftyHazard_RR_Links.Count();
            sh.LinkRegRequirement(rr);
            sh.UnLinkRegRequirement(rr);
            Assert.Equal(rrCount, sh.SaftyHazard_RR_Links.Count());
        }
    }
}
