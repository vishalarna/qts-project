using QTD2.Test.Data.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class SafetyHazard_CategoryTests
    {
        public static IEnumerable<object[]> GetSafetyHazard_CategoryTestData()
        {
            var data = new List<object[]>();
            var saftyHazard_cat = SafetyHazardCategoryTestData.GetAll();
            var safetyHazards = SafetyHazardTestData.GetAll();

            foreach (var cat in saftyHazard_cat)
            {
                foreach (var sh in safetyHazards)
                {
                    data.Add(new object[] { cat, sh });
                }
            }

            return data;
        }
    }
}
