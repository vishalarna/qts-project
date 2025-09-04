using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class SafetyHazard_AbatementTestData
    {
        public static List<SaftyHazard_Abatement> GetAll()
        {
            return new List<SaftyHazard_Abatement>()
            {
                SH_Abatement1(),
                SH_Abatement2(),
            };
        }

        static SaftyHazard_Abatement SH_Abatement1()
        {
            return new SaftyHazard_Abatement(1, 1, "Abatement Test Data");
        }

        static SaftyHazard_Abatement SH_Abatement2()
        {
            return new SaftyHazard_Abatement(1, 2, "Another Abatement Test Data");
        }
    }
}
