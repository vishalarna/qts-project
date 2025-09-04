using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class SafetyHazard_ControlTestData
    {
        public static List<SaftyHazard_Control> GetAll()
        {
            return new List<SaftyHazard_Control>()
            {
                SH_Control1(),
                SH_Control2(),
            };

        }

        static SaftyHazard_Control SH_Control1()
        {
            return new SaftyHazard_Control(1, 1, "SH Control Test");
        }
        static SaftyHazard_Control SH_Control2()
        {
            return new SaftyHazard_Control(1, 2, "Another SH Control Test");
        }
    }
}
