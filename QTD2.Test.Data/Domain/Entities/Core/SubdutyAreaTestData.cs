using QTD2.Domain.Entities.Core;
using System.Collections.Generic;

namespace QTD2.Test.Data.Domain.Entities.Core
{
    public static class SubdutyAreaTestData
    {
        public static List<SubdutyArea> GetAll()
        {
            return new List<SubdutyArea>()
            {
                Sda1(),
                Sda2(),
            };
        }

        static SubdutyArea Sda1()
        {
            return new SubdutyArea(1, "Voltage Monitoring", 1, "Test Title", System.DateTime.Now, "Reason");
        }

        static SubdutyArea Sda2()
        {
            return new SubdutyArea(1, "Supervision", 2, "Test Title", System.DateTime.Now, "Reason");
        }
    }
}
