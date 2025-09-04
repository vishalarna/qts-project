using QTD2.Domain.Entities.Core;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class SafetyHazardTests
    {
        [Theory, MemberData(nameof(SafetyHazardTests.GetSafetyHazard_AbatementData))]
        public void SafetyHazard_AddAbatement(SaftyHazard saftyHazard, SaftyHazard_Abatement saftyHazard_Abatement)
        {
            var sh_aCount = saftyHazard.SaftyHazard_Abatements.Count;

            saftyHazard.AddAbatement(saftyHazard_Abatement);

            Assert.Equal(sh_aCount + 1, saftyHazard.SaftyHazard_Abatements.Count);
        }

        [Theory, MemberData(nameof(SafetyHazardTests.GetSafetyHazard_AbatementData))]
        public void SafetyHazard_RemoveAbatement(SaftyHazard saftyHazard, SaftyHazard_Abatement saftyHazard_Abatement)
        {
            var sh_aCount = saftyHazard.SaftyHazard_Abatements.Count;

            saftyHazard.AddAbatement(saftyHazard_Abatement);
            saftyHazard.RemoveAbatement(saftyHazard_Abatement);

            Assert.Equal(sh_aCount, saftyHazard.SaftyHazard_Abatements.Count);
        }
    }
}
