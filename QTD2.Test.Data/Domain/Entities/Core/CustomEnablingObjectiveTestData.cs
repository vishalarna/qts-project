using QTD2.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class CustomEnablingObjectiveTestData
    {
        public static List<CustomEnablingObjective> GetAll()
        {
            return new List<CustomEnablingObjective>()
            {
                EO1(),
                EO2(),
            };
        }

        static CustomEnablingObjective EO1()
        {
            var eo = new CustomEnablingObjective(1, "Test EO", false);
            eo.Set_Id(1);
            return eo;
        }

        static CustomEnablingObjective EO2()
        {
            var eo = new CustomEnablingObjective(2, "Another EnablingObjective", false);
            eo.Set_Id(2);
            return eo;
        }
    }
}
