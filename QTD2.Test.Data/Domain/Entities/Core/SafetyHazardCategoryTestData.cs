using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class SafetyHazardCategoryTestData
    {
        public static List<SaftyHazard_Category> GetAll()
        {
            return new List<SaftyHazard_Category>()
            {
                SH_Cat1()
            };
        }

        static SaftyHazard_Category SH_Cat1()
        {
            return new SaftyHazard_Category("Safety Gadgets", 1, "Safety Hazard title", "Safety Hazard Notes", DateTime.Now);
        }
    }
}
