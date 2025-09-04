using QTD2.Domain.Entities.Core;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class SafetyHazardTests
    {

        [Theory, MemberData(nameof(SafetyHazardTests.GetSafetyHazard_ControlData))]
        public void SafetyHazard_AddControl(SaftyHazard saftyHazard, SaftyHazard_Control saftyHazard_Control)
        {
            var sh_cCount = saftyHazard.SaftyHazard_Controls.Count;

            saftyHazard.AddControl(saftyHazard_Control);

            Assert.Equal(sh_cCount + 1, saftyHazard.SaftyHazard_Controls.Count);
        }

        [Theory, MemberData(nameof(SafetyHazardTests.GetSafetyHazard_ControlData))]
        public void SafetyHazard_RemoveControl(SaftyHazard saftyHazard, SaftyHazard_Control saftyHazard_Control)
        {
            var sh_cCount = saftyHazard.SaftyHazard_Controls.Count;

            saftyHazard.AddControl(saftyHazard_Control);
            saftyHazard.RemoveControl(saftyHazard_Control);

            Assert.Equal(sh_cCount, saftyHazard.SaftyHazard_Controls.Count);
        }
    }
}
