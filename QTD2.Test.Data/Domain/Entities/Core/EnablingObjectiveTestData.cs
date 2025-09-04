using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class EnablingObjectiveTestData
    {
        public static List<EnablingObjective> GetAll()
        {
            return new List<EnablingObjective>()
            {
                EO1(),
                EO2(),
            };
        }

        static EnablingObjective EO1()
        {
            var eo = new EnablingObjective(1, 1, 1, "1.1.1.1", "s", true, false, "", "s", "s", DateTime.Now);
            eo.Set_Id(1);
            return eo;
        }

        static EnablingObjective EO2()
        {
            var eo = new EnablingObjective(1, 1, 2, "1.1.1.2", "s", false, true, "s", "s", "", DateTime.Now);
            eo.Set_Id(2);
            return eo;
        }
    }
}
