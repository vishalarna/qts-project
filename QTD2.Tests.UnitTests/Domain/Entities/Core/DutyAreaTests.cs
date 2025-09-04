using QTD2.Infrastructure.ExtensionMethods;
using QTD2.Test.Data.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class DutyAreaTests
    {
        public static IEnumerable<object[]> GetSubDutyAreaData()
        {
            var dutyArea = DutyAreaTestData.GetAll();
            var subDutyArea = SubdutyAreaTestData.GetAll();

            var data = new List<object[]>();

            foreach (var da in dutyArea)
            {
                foreach (var sda in subDutyArea)
                {
                    data.Add(new object[] { da.DeepCopy(), sda.DeepCopy() });
                }
            }

            return data;
        }
    }
}
