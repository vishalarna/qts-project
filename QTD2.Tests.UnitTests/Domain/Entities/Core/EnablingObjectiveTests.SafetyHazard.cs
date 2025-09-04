using QTD2.Domain.Entities.Core;
using System.Linq;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class EnablingObjectiveTests
    {

        [Theory, MemberData(nameof(EnablingObjectiveTests.GetEnablingObjective_SafetyHazardTestData))]
        public void EnablingObjectiveTests_LinkSafetyHazard(EnablingObjective eo, SaftyHazard saftyHazard)
        {
            var shCount = eo.SafetyHazard_EO_Links.Count();

            eo.LinkSaftyHazard(saftyHazard, eo.Id);

            Assert.Equal(shCount + 1, eo.SafetyHazard_EO_Links.Count());
        }

        [Theory, MemberData(nameof(EnablingObjectiveTests.GetEnablingObjective_SafetyHazardTestData))]
        public void EnablingObjectiveTests_UnLinkSafetyHazard(EnablingObjective eo, SaftyHazard saftyHazard)
        {
            var shCount = eo.SafetyHazard_EO_Links.Count();

            eo.LinkSaftyHazard(saftyHazard, eo.Id);
            eo.UnlinkSaftyHazard(saftyHazard);
            Assert.Equal(shCount, eo.SafetyHazard_EO_Links.Count());
        }
    }
}
