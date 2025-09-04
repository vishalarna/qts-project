using QTD2.Domain.Entities.Core;
using System.Linq;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class SafetyHazardTests
    {
        [Theory, MemberData(nameof(SafetyHazardTests.GetSafetyHazard_ILA))]
        public void SafetyHazard_AddILA(ILA ila, SaftyHazard sh)
        {
            var shCount = sh.SafetyHazard_ILA_Links.Count();
            sh.LinkILA(ila);
            Assert.Equal(shCount + 1, sh.SafetyHazard_ILA_Links.Count());
        }
        [Theory, MemberData(nameof(SafetyHazardTests.GetSafetyHazard_ILA))]
        public void SafetyHazard_RemoveILA(ILA ila, SaftyHazard sh)
        {
            var shCount = sh.SafetyHazard_ILA_Links.Count();
            sh.LinkILA(ila);
            sh.UnLinkILA(ila);
            Assert.Equal(shCount, sh.SafetyHazard_ILA_Links.Count());
        }
    }
}
