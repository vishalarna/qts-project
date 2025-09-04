using QTD2.Domain.Entities.Core;
using System.Linq;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class DutyAreaTests
    {
        [Theory, MemberData(nameof(DutyAreaTests.GetSubDutyAreaData))]
        public void DutyAreaTests_AddSubDutyArea(DutyArea dutyArea, SubdutyArea subdutyArea)
        {

            var sdaCount = dutyArea.SubdutyAreas.Count();

            dutyArea.AddSubduty(subdutyArea);

            Assert.Equal(sdaCount + 1, dutyArea.SubdutyAreas.Count());
        }

        [Theory, MemberData(nameof(DutyAreaTests.GetSubDutyAreaData))]
        public void DutyAreaTests_RemoveSubDutyArea(DutyArea dutyArea, SubdutyArea subdutyArea)
        {

            var sdaCount = dutyArea.SubdutyAreas.Count();

            dutyArea.AddSubduty(subdutyArea);
            dutyArea.RemoveSubduty(subdutyArea);

            Assert.Equal(sdaCount, dutyArea.SubdutyAreas.Count());
        }
    }
}
