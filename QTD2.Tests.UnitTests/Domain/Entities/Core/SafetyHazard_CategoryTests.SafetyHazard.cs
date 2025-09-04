using QTD2.Domain.Entities.Core;
using Xunit;

namespace QTD2.Tests.UnitTests.Domain.Entities.Core
{
    public partial class SafetyHazard_CategoryTests
    {
        //[Theory, MemberData(nameof(SafetyHazard_CategoryTests.GetSafetyHazard_CategoryTestData))]
        //public void SafetyHazard_Category_AddSafetyHazard(SaftyHazard_Category saftyHazard_Category, SaftyHazard saftyHazard)
        //{
        //    var shCount = saftyHazard_Category.SaftyHazards.Count;

        //    saftyHazard_Category.AddSaftyHazard(saftyHazard);

        //    Assert.Equal(shCount + 1, saftyHazard_Category.SaftyHazards.Count);
        //}

        [Theory, MemberData(nameof(SafetyHazard_CategoryTests.GetSafetyHazard_CategoryTestData))]
        public void SafetyHazard_Category_RemoveSafetyHazard(SaftyHazard_Category saftyHazard_Category, SaftyHazard saftyHazard)
        {
            var shCount = saftyHazard_Category.SaftyHazards.Count;

            saftyHazard_Category.AddSaftyHazard(saftyHazard);
            saftyHazard_Category.RemoveSaftyHazard(saftyHazard);

            Assert.Equal(shCount, saftyHazard_Category.SaftyHazards.Count);
        }
    }
}
